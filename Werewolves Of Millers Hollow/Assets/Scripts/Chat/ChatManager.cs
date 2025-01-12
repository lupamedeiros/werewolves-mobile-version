using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public PhotonView photonView;
    [Header("UI Elements")]
    public TMP_InputField chatInputField; 
    public TextMeshProUGUI chatDisplay;      
    public ScrollRect scrollRect;
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Você precisa estar conectado ao Photon para usar o chat.");
            return;
        }
    }
    
    

    public void OnSendMessage()
    {
        string message = chatInputField.text;
        if (!string.IsNullOrEmpty(message))
        {
            photonView.RPC("BroadcastMessage", RpcTarget.All, PhotonNetwork.NickName, message);
            chatInputField.text = "";
        }
    }


    [PunRPC]
    void BroadcastMessage(string sender, string message)
    {
        chatDisplay.text += $"\n<b>{sender}:</b> {message}";
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatDisplay.rectTransform);
        ScrollToBottom();
    }

    void ScrollToBottom()
    {
        StartCoroutine(ScrollToBottomNextFrame());
    }

    private IEnumerator ScrollToBottomNextFrame()
    {
        yield return null; 
        scrollRect.verticalNormalizedPosition = 0; 
    }
}
