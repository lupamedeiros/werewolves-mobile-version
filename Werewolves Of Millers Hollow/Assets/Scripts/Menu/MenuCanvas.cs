using Game.Multiplayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Menu
{
    public class MenuCanvas : MonoBehaviour
    {
        [Header("Connect Settings")]
        [SerializeField] ConnectToServer m_multiplayerLogic;
        bool m_connecting = false;

        [SerializeField] InputField m_nicknameInputField;
        [Header("Connect Button Settings")]
        [SerializeField] Button m_connectToLobbyButton;
        [SerializeField] TMPro.TextMeshProUGUI m_connectToLobbyButtonText;
        [SerializeField] TMPro.TextMeshProUGUI m_failedText;
        [SerializeField] string m_defaultConnectButtonText;
        private void OnEnable()
        {
            m_connecting = false;
            ClearErrorMessage();
            ResetConnectButtonText();
            m_connectToLobbyButton.onClick.AddListener(Connect);
            MultiplayerObserver.OnFailedToConnectToLobby += SetFailedMessage;
        }

        private void OnDisable()
        {
            m_connectToLobbyButton.onClick.RemoveListener(Connect);
            MultiplayerObserver.OnFailedToConnectToLobby -= SetFailedMessage;
        }

        void SetFailedMessage(string message)
        {
            m_failedText.text = message;
            m_failedText.gameObject.SetActive(true);
            m_connecting = false;
            ResetConnectButtonText();
        }

        void ClearErrorMessage()
        {
            m_failedText.text = string.Empty;
            m_failedText.gameObject.SetActive(false);
        }

        void ResetConnectButtonText() => UpdateConnectButtonText(m_defaultConnectButtonText);
        void UpdateConnectButtonText(string text) => m_connectToLobbyButtonText.text = text;

        void Connect()
        {
            if (!m_nicknameInputField) return;
            if (m_connecting) return;
            m_connecting = true;
            ClearErrorMessage();
            UpdateConnectButtonText("Entrando...");
            string nickname = m_nicknameInputField.text;
            m_multiplayerLogic.ConnectWithNickname(nickname);
        }
    }
}