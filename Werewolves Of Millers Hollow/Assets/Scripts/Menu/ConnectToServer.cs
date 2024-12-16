using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Game.Multiplayer
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        public void ConnectWithNickname(string nickname)
        {
            if (string.IsNullOrWhiteSpace(nickname))
            {
                FailedToConnectToLobby("Não é possível conectar sem um nome!");
                return;
            }
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = nickname;
            PhotonNetwork.ConnectUsingSettings();
            MultiplayerObserver.TryConnectToLobby();
        }

        void FailedToConnectToLobby(string message)
        {
            Debug.LogWarning(message);
            MultiplayerObserver.FailedToConnectToLobby(message);
        }

        public override void OnConnectedToMaster()
        {
            GameSceneManager.GetInstance(false).LoadLobbyScene();
            MultiplayerObserver.ConnectToMaster();
        }
    }
}