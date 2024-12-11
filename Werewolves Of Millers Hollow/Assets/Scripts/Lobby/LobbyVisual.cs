using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lobby
{
    public class LobbyVisual : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI m_nickname;

        private void OnEnable()
        {
            SetNickname();
        }

        void SetNickname()
        {
            string nickname = PhotonNetwork.NickName;
            m_nickname.text = $"Apelido: {nickname}";
        }
    }
}