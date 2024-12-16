using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Lobby
{
    public class RoomEnter : MonoBehaviourPunCallbacks
    {
        [Header("UI Settings")]
        [SerializeField] RoomItem m_roomItemPrefab;
        [SerializeField] float m_yButtonSize;
        [SerializeField] InputField m_roomCodeInputField;
        List<RoomItem> m_roomItems = new();
        [SerializeField] RectTransform m_contentObject;
        [SerializeField] Button m_refreshButton;
        [SerializeField] Button m_enterRoomByCodeButton;

        [Header("Connection Settings")]
        [SerializeField, Min(0)] float m_secondsToRefresh;
        float m_refreshTime;
        List<RoomInfo> m_currentRoomList = new();
        bool m_enteringRoom;

        private void Awake()
        {
            (m_roomItems ??= new()).Clear();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            m_refreshButton.onClick.AddListener(UpdateRoomVisual);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            m_refreshButton.onClick.RemoveListener(UpdateRoomVisual);
        }

        private void Update()
        {
            if (Time.time >= m_refreshTime)
            {
                UpdateRoomVisual();
            }

            m_enterRoomByCodeButton.gameObject.SetActive(CanEnterRoom());
        }

        void EnterRoomByCode()
        {
            if (!CanEnterRoom()) return;
            m_enteringRoom = true;
            Debug.Log($"Entrando na sala: {GetInputFieldRoomName()}!");
            MultiplayerObserver.EnteringRoom();
            PhotonNetwork.JoinRoom(GetInputFieldRoomName());
        }

        bool CanEnterRoom()
        {
            if (string.IsNullOrWhiteSpace(GetInputFieldRoomName())) return false;
            if (m_enteringRoom) return false;
            RoomInfo room = m_currentRoomList.Find(x => x.Name == GetInputFieldRoomName());
            if (room == null) return false;
            if (!room.IsVisible) return false;
            if (!room.IsOpen) return false;
            if (room.PlayerCount >= room.MaxPlayers) return false;
            return true;
        }

        string GetInputFieldRoomName() => m_roomCodeInputField.text;

        void CreateRoomButtons(List<RoomInfo> roomList)
        {
            foreach (RoomItem room in m_roomItems)
            {
                Destroy(room.gameObject);
            }
            m_roomItems.Clear();

            foreach (RoomInfo room in roomList)
            {
                if (!room.IsVisible) continue;
                if (!room.IsOpen) continue;
                if (room.PlayerCount == room.MaxPlayers) continue;

                RoomItem newRoom = Instantiate(m_roomItemPrefab, m_contentObject);
                newRoom.SetRoomName(room.Name);
                m_roomItems.Add(newRoom);
                Debug.Log($"Exibindo conexão da sala: {room.Name}");
            }

            float x = m_contentObject.sizeDelta.x;
            float y = m_yButtonSize * m_roomItems.Count;
            m_contentObject.sizeDelta.Set(x, y);
            Debug.Log("Visualização de salas atualizada!");
        }

        public void UpdateRoomVisual()
        {
            Debug.Log("Atualizando visualização de salas!");
            m_currentRoomList ??= new();
            CreateRoomButtons(m_currentRoomList);
            m_refreshTime = Time.time + m_secondsToRefresh;
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            //base.OnRoomListUpdate(roomList);
            Debug.Log("Lista de salas ataualizada!");
            m_currentRoomList = roomList;
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            m_enteringRoom = false;
            UpdateRoomVisual();
        }
    }
}