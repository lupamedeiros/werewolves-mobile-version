using Game.Multiplayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

namespace Game.Menu
{
    public class MenuCanvas : MonoBehaviour
    {
        [Header("Connect Settings")]
        [SerializeField] ConnectToServer m_multiplayerLogic;

        [SerializeField] TMPro.TMP_InputField m_nicknameInputField;
        [Header("Connect Button Settings")]
        [SerializeField] Button m_connectToLobbyButton;
        [SerializeField] TMPro.TextMeshProUGUI m_connectToLobbyButtonText;
        [SerializeField] TMPro.TextMeshProUGUI m_failedText;
        [SerializeField] string m_defaultConnectButtonText;
        private void OnEnable()
        {
            SetFailedMessage();
            ResetConnectButtonText();
            m_connectToLobbyButton.onClick.AddListener(Connect);
            MultiplayerObserver.OnFailedToConnectToLobby += SetFailedMessage;
        }

        private void OnDisable()
        {
            m_connectToLobbyButton.onClick.RemoveListener(Connect);
            MultiplayerObserver.OnFailedToConnectToLobby -= SetFailedMessage;
        }

        void SetFailedMessage(string message = null)
        {
            m_failedText.text = message;
            m_failedText.gameObject.SetActive(!string.IsNullOrWhiteSpace(message));
            ResetConnectButtonText();
        }

        void ResetConnectButtonText() => UpdateConnectButtonText(m_defaultConnectButtonText);
        void UpdateConnectButtonText(string text) => m_connectToLobbyButtonText.text = text;

        void Connect()
        {
            SetFailedMessage();
            UpdateConnectButtonText("Connecting...");
            string nickname = m_nicknameInputField.text;
            m_multiplayerLogic.ConnectWithNickname(nickname);
        }
    }
}