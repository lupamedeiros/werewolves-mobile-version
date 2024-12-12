using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lobby
{
    public class LobbyController : MonoBehaviourPunCallbacks
    {
        [Header("Editor Only")]
        [SerializeField] bool m_setOnAwake = true;
        

        [Header("Canvas")]
        [SerializeField] GameObject m_lobbyCanvas;
        [SerializeField] GameObject m_createRoomCanvas;
        [SerializeField] GameObject m_enterRoomCanvas;

        [SerializeField] GameObject m_defaultCanvas;

        [Header("Lobby Components")]
        [SerializeField] RoomEnter m_enterTab;

        private void Awake()
        {
#if UNITY_EDITOR
            if (m_setOnAwake)
            {
                PhotonNetwork.NickName = "Dev Testing";
                PhotonNetwork.ConnectUsingSettings();
            }
#endif

            EnableCanvas(m_defaultCanvas);
            PhotonNetwork.JoinLobby();
        }

        public void EnableLobby() => EnableCanvas(m_lobbyCanvas);
        public void EnableCreateRoom() => EnableCanvas(m_createRoomCanvas);
        public void EnableEnterRoom() => EnableCanvas(m_enterRoomCanvas);

        void EnableCanvas(GameObject canvas)
        {
            m_lobbyCanvas.SetActive(m_lobbyCanvas == canvas);
            m_createRoomCanvas.SetActive(m_createRoomCanvas == canvas);
            m_enterRoomCanvas.SetActive(m_enterRoomCanvas == canvas);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("ENTROU NA SALA PORRA");
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            m_enterTab.UpdateRoomList(roomList);
        }
    }
}