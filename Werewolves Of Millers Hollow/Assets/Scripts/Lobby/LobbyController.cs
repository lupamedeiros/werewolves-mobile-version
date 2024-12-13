using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lobby
{
    public class LobbyController : MonoBehaviourPunCallbacks
    {
        [Header("Canvas")]
        [SerializeField] GameObject m_lobbyCanvas;
        [SerializeField] GameObject m_createRoomCanvas;
        [SerializeField] GameObject m_enterRoomCanvas;

        [SerializeField] GameObject m_defaultCanvas;
        bool m_canSwitchCanvas;

        private void Awake()
        {
#if UNITY_EDITOR
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.NickName = "Dev Testing";
                PhotonNetwork.ConnectUsingSettings();
            }
#endif
            EnableSwitchCanvas();
            EnableCanvas(m_defaultCanvas);      
        }
        public override void OnEnable()
        {
            base.OnEnable();
            MultiplayerObserver.OnEnteringRoom += DisableSwitchCanvas;
        }
        public override void OnDisable()
        {
            base.OnDisable();
            MultiplayerObserver.OnEnteringRoom -= DisableSwitchCanvas;

        }
        

        public void EnableLobby() => EnableCanvas(m_lobbyCanvas);
        public void EnableCreateRoom() => EnableCanvas(m_createRoomCanvas);
        public void EnableEnterRoom() => EnableCanvas(m_enterRoomCanvas);
        void EnableCanvas(GameObject canvas)
        {
            if (!m_canSwitchCanvas) return;
            m_lobbyCanvas.SetActive(m_lobbyCanvas == canvas);
            m_createRoomCanvas.SetActive(m_createRoomCanvas == canvas);
            m_enterRoomCanvas.SetActive(m_enterRoomCanvas == canvas);
        }
        void EnableSwitchCanvas()
        {
            m_canSwitchCanvas = true;
        }
        void DisableSwitchCanvas()
        {
            m_canSwitchCanvas = false;
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("Entrou na sala!");
            GameSceneManager.GetInstance(false).LoadGameHubScene();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            EnableSwitchCanvas();
        }
    }
}