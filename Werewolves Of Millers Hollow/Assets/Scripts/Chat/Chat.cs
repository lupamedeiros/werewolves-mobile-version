using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Chat : MonoBehaviourPunCallbacks
{
    [Header("UI")]
    public TextMeshProUGUI chat; // Exibe as mensagens no chat
    public TMP_InputField inputField; // Campo de entrada para digitar mensagens

    private const byte ChatEventCode = 1; // Código único para o evento de chat

    void Start()
    {
        // Verifica se o jogador está conectado ao Photon
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Você precisa estar conectado ao Photon para usar o chat.");
            return;
        }

        Debug.Log("Conectado ao Photon. Pronto para enviar mensagens!");
    }

    public void InputChat()
    {
        string txt = inputField.text; // Obtém o texto digitado

        if (!string.IsNullOrEmpty(txt)) // Verifica se o texto não está vazio
        {
            SendMessageToAll(txt); // Envia a mensagem para todos os jogadores

            inputField.text = ""; // Limpa o campo de entrada
            inputField.ActivateInputField(); // Mantém o foco no campo de entrada
        }
        else
        {
            Debug.LogWarning("Mensagem vazia. Nada será enviado.");
        }
    }

    public void SendMessageToAll(string message)
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Photon não está conectado. A mensagem não pode ser enviada.");
            return;
        }

        // Atualiza localmente o texto no chat
        chat.text += $"\nEu: {message}";

        // Envia a mensagem pela rede usando o evento RaiseEvent
        PhotonNetwork.RaiseEvent(
            ChatEventCode, // Código do evento
            message, // Dados da mensagem
            new RaiseEventOptions { Receivers = ReceiverGroup.All }, // Enviar para todos
            SendOptions.SendReliable // Envio confiável
        );

        Debug.Log($"Mensagem enviada pela rede: {message}");
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.EventReceived += OnEventReceived; // Registra o evento de recebimento
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.NetworkingClient.EventReceived -= OnEventReceived; // Remove o registro do evento
    }

    private void OnEventReceived(ExitGames.Client.Photon.EventData photonEvent)
    {
        if (photonEvent.Code == ChatEventCode) // Verifica se é o evento de chat
        {
            string message = (string)photonEvent.CustomData;
            chat.text += $"\nOutro jogador: {message}";

            Debug.Log($"Mensagem recebida pela rede: {message}");
        }
    }
}
