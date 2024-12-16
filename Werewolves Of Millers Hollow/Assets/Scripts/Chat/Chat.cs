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
    public TextMeshProUGUI chat; 

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Você precisa estar conectado ao Photon para usar o chat.");
            return;
        }
    }


    public void InputChat(string txt)
    {
        if (!string.IsNullOrEmpty(txt))
        {
            SendMessageToAll(txt);
        }
    }


    public void SendMessageToAll(string message)
    {
        
        chat.text += $"\nEu: {message}";

        
        PhotonNetwork.RaiseEvent(
            1, 
            message, 
            new RaiseEventOptions { Receivers = ReceiverGroup.All },
            SendOptions.SendReliable
        );
    }
    
    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.EventReceived += OnEventReceived;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.NetworkingClient.EventReceived -= OnEventReceived;
    }

    private void OnEventReceived(ExitGames.Client.Photon.EventData photonEvent)
    {
        if (photonEvent.Code == 1)
        {
            string message = (string)photonEvent.CustomData;
            chat.text += $"\nOutro jogador: {message}";
        }
    }
}