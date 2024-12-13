using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lobby
{
    public class RoomItem : MonoBehaviourPunCallbacks
    {
        [SerializeField] UnityEngine.UI.Button m_button;
        string m_roomName;
        [SerializeField] TMPro.TextMeshProUGUI m_roomNameText;
        bool m_enteringRoom = false;

        public override void OnEnable()
        {
            base.OnEnable();
            m_enteringRoom = false;
            m_button.onClick.AddListener(JoinRoom);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            m_button.onClick.RemoveListener(JoinRoom);
        }

        public void SetRoomName(string newRoomName)
        {
            m_roomName = newRoomName;
            m_roomNameText.text = m_roomName;
        }

        void JoinRoom()
        {
            if (m_enteringRoom) return;
            m_enteringRoom = true;
            Debug.Log($"Entrando na sala: {m_roomName}!");
            MultiplayerObserver.EnteringRoom();
            PhotonNetwork.JoinRoom(m_roomName);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            Debug.LogError($"Entrar na sala falhou! {message}");
            m_enteringRoom = false;
        }
    }
}