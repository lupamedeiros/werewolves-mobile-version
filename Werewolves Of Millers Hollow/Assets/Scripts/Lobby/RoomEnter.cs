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
        List<RoomItem> m_roomItems = new();
        [SerializeField] RectTransform m_contentObject;
        [SerializeField] Button m_refreshButton;

        [Header("Connection Settings")]
        [SerializeField, Min(0)] float m_secondsToRefresh;
        float m_refreshTime;
        List<RoomInfo> m_currentRoomList = new();

        private void Awake()
        {
            (m_roomItems ??= new()).Clear();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            m_refreshButton.onClick.AddListener(UpdateRoomList);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            m_refreshButton.onClick.RemoveListener(UpdateRoomList);
        }

        private void Update()
        {
            if (Time.time >= m_refreshTime)
            {
                UpdateRoomList();
            }
        }

        void UpdateRoomListVisual(List<RoomInfo> roomList)
        {
            foreach (RoomItem room in m_roomItems)
            {
                Destroy(room.gameObject);
            }
            m_roomItems.Clear();

            foreach (RoomInfo room in roomList)
            {
                RoomItem newRoom = Instantiate(m_roomItemPrefab, m_contentObject);
                newRoom.SetRoomName(room.Name);
                m_roomItems.Add(newRoom);
                Debug.Log($"Exibindo conexão da sala: {room.Name}");
            }

            float x = m_contentObject.sizeDelta.x;
            float y = m_yButtonSize * m_roomItems.Count;
            m_contentObject.sizeDelta = new Vector2(x, y);
            Debug.Log("Visualização de salas atualizada!");
        }

        void UpdateRoomList()
        {
            Debug.Log("Atualizando visualização de salas!");
            m_currentRoomList ??= new();
            UpdateRoomListVisual(m_currentRoomList);
            m_refreshTime = Time.time + m_secondsToRefresh;
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
            Debug.Log("Lista de salas ataualizada!");
            m_currentRoomList = roomList;
        }
    }
}