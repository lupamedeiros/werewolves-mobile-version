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
    [SerializeField] private ScrollRect scrollRect;
    private bool isUserScrolling;
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Você precisa estar conectado ao Photon para usar o chat.");
            return;
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            isUserScrolling = true;
        }
        else
        {
            isUserScrolling = false;
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
        
        Canvas.ForceUpdateCanvases(); 
        if (!isUserScrolling)
        {
            scrollRect.verticalNormalizedPosition = 0; 
        }
    }
}
