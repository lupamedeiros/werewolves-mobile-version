using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameRoom
{
    public class RoomLabel : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI m_roomName;

        private void OnEnable()
        {
            UpdateVisual();
        }

        void UpdateVisual()
        {
            m_roomName.text = $"Nome da sala: {PhotonNetwork.CurrentRoom.Name}";
        }
    }
}