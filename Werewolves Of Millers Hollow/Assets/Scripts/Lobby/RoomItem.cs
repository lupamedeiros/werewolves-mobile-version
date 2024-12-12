using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lobby
{
    public class RoomItem : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Button m_button;
        string m_roomName;
        [SerializeField] TMPro.TextMeshProUGUI m_roomNameText;

        private void OnEnable()
        {
            m_button.onClick.AddListener(JoinRoom);
        }

        private void OnDisable()
        {
            m_button.onClick.RemoveListener(JoinRoom);
        }

        public void SetRoomName(string newRoomName)
        {
            m_roomName = newRoomName;
            m_roomNameText.text = m_roomName;
        }

        void JoinRoom()
        {
            PhotonNetwork.JoinRoom(m_roomName);
        }
    }
}