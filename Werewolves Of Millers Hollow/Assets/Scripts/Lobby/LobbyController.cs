using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lobby
{
    public class LobbyController : MonoBehaviour
    {
        [Header("Canvas")]
        [SerializeField] GameObject m_lobbyCanvas;
        [SerializeField] GameObject m_createRoomCanvas;
        [SerializeField] GameObject m_enterRoomCanvas;

        [SerializeField] GameObject m_defaultCanvas;

        private void OnEnable()
        {
            EnableCanvas(m_defaultCanvas);
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
    }
}