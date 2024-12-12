using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Lobby
{
    public class RoomCreator : MonoBehaviour
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

        public void OnEnable()
        {
            ResetCreator();
        }

        void ResetCreator()
        {
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
            string roomName = m_roomNameInputField.text;
            if (string.IsNullOrWhiteSpace(roomName)) return;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = m_currentMaxPlayerCount;
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
    }
}