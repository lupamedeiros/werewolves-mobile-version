using Game.Multiplayer;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameRoom
{
    public class StartRoomButton : MonoBehaviour
    {
        [SerializeField] Button m_startButton;

        private void OnEnable()
        {
            m_startButton.onClick.AddListener(StartRoom);
        }

        private void OnDisable()
        {
            m_startButton.onClick.RemoveListener(StartRoom);
        }

        void StartRoom()
        {
            if (!CanStartRoom()) return;
            GameSceneManager.Instance.LoadGameScene();
        }

        bool CanStartRoom()
        {
            if (!PhotonNetwork.IsMasterClient) return false;

            int minPlayerCount = PropertiesHandler.GetRoomPropertyValue<int>(PropertiesHandler.PROP_ROOM_MINPLAYERCOUNT);
            if (PhotonNetwork.CurrentRoom.PlayerCount < minPlayerCount) return false;

            return true;
        }

        private void Update()
        {
            m_startButton.gameObject.SetActive(CanStartRoom());
        }
    }
}