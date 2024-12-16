using Game.Multiplayer;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Lobby
{
    public class RoomCreator : MonoBehaviourPunCallbacks
    {
        [Header("Room Name")]
        [SerializeField] string m_defaultRoomName;
        [SerializeField] InputField m_roomNameInputField;

        [Header("Min Player Count")]
        [SerializeField] TMPro.TextMeshProUGUI m_minPlayerCountIndicator;
        [SerializeField, Min(0)] int m_defaultMinPlayerCount = 1;
        [SerializeField, Min(1)] int m_minPlayerCount = 1;
        int m_currentMinPlayerCount;

        [Header("Max Player Count")]
        [SerializeField] TMPro.TextMeshProUGUI m_maxPlayerCountIndicator;
        [SerializeField, Min(0)] int m_defaultMaxPlayerCount = 1;
        [SerializeField, Min(1)] int m_maxPlayerCount = 1;
        int m_currentMaxPlayerCount;

        [Header("Create room button")]
        [SerializeField] Button m_createRoomButton;
        bool m_creatingRoom;
        List<RoomInfo> m_currentRooms;

        private void Awake()
        {
            m_currentRooms ??= new List<RoomInfo>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            m_createRoomButton.onClick.AddListener(CreateRoom);
            ResetCreator();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            m_createRoomButton.onClick.RemoveListener(CreateRoom);
            ResetCreator();
        }

        private void Update()
        {
            m_createRoomButton.gameObject.SetActive(CanCreateRoom());
        }

        bool CanCreateRoom()
        {
            if (string.IsNullOrWhiteSpace(GetRoomName())) return false;
            if (PhotonNetwork.Server != ServerConnection.MasterServer) return false;
            if (!PhotonNetwork.IsConnectedAndReady) return false;
            if (PhotonNetwork.NetworkClientState == ClientState.Authenticating) return false;
            if (PhotonNetwork.NetworkClientState == ClientState.ConnectingToGameServer) return false;
            if (PhotonNetwork.NetworkClientState == ClientState.JoiningLobby) return false;
            if (PhotonNetwork.NetworkClientState == ClientState.Joining) return false;
            if (PhotonNetwork.NetworkClientState == ClientState.Disconnecting) return false;
            if (m_currentRooms.Exists(x => x.Name == GetRoomName())) return false;
            if (m_creatingRoom) return false;
            return true;
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
            m_currentRooms = roomList;
        }

        void ResetCreator()
        {
            m_creatingRoom = false;
            m_roomNameInputField.text = m_defaultRoomName;
            m_currentMinPlayerCount = m_defaultMinPlayerCount;
            m_currentMaxPlayerCount = m_defaultMaxPlayerCount;
            UpdatePlayerCountIndicator();
        }

        public void AddMinPlayerCount()
        {
            if (m_currentMinPlayerCount >= m_currentMaxPlayerCount) return;
            m_currentMinPlayerCount++;
            UpdatePlayerCountIndicator();
        }

        public void RemoveMinPlayerCount()
        {
            if (m_currentMinPlayerCount <= m_minPlayerCount) return;
            m_currentMinPlayerCount--;
            UpdatePlayerCountIndicator();
        }

        public void AddMaxPlayerCount()
        {
            if (m_currentMaxPlayerCount >= m_maxPlayerCount) return;
            m_currentMaxPlayerCount++;
            UpdatePlayerCountIndicator();
        }

        public void RemoveMaxPlayerCount()
        {
            if (m_currentMaxPlayerCount <= m_currentMinPlayerCount) return;
            m_currentMaxPlayerCount--;
            UpdatePlayerCountIndicator();
        }

        void UpdatePlayerCountIndicator()
        {
            m_minPlayerCountIndicator.text = $"{m_currentMinPlayerCount}";
            m_maxPlayerCountIndicator.text = $"{m_currentMaxPlayerCount}";
        }

        public void CreateRoom()
        {
            if (!CanCreateRoom()) return;
            Debug.Log("Criando sala!");
            MultiplayerObserver.EnteringRoom();
            m_creatingRoom = true;
            PhotonNetwork.CreateRoom(GetRoomName(), GetNewRoomOptions());
        }

        RoomOptions GetNewRoomOptions()
        {
            RoomOptions roomOptions = new RoomOptions()
            {
                IsOpen = true,
                IsVisible = true,
                MaxPlayers = m_currentMaxPlayerCount
            };

            ExitGames.Client.Photon.Hashtable customRoomProps = new ExitGames.Client.Photon.Hashtable()
            {
                { PropertiesHandler.PROP_ROOM_MINPLAYERCOUNT, m_currentMinPlayerCount }
            };
            
            roomOptions.CustomRoomProperties = customRoomProps;
            return roomOptions;
        }

        public string GetRoomName() => m_roomNameInputField.text;

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            Debug.Log("Sala criada!");
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            m_creatingRoom = false;
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            m_creatingRoom = false;
        }
    }
}