using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameRoom
{
    public class UpdatePlayerList : MonoBehaviourPunCallbacks
    {
        [Header("UI Settings")]
        [SerializeField] TMPro.TextMeshProUGUI m_textPrefab;
        [SerializeField] RectTransform m_content;

        [Header("Content Size Settings")]
        [SerializeField, Min(0)] float m_textHeight;
        [SerializeField] float m_topPadding;
        [SerializeField, Min(0)] float m_spacing;

        List<GameObject> m_currentPlayerList;

        private void Start()
        {
            UpdatePlayerListVisual();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            UpdatePlayerListVisual();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            UpdatePlayerListVisual();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            UpdatePlayerListVisual();
        }

        void UpdatePlayerListVisual()
        {
            m_currentPlayerList ??= new();
            foreach (GameObject obj in m_currentPlayerList)
            {
                Destroy(obj);
            }
            m_currentPlayerList.Clear();
            if (PhotonNetwork.CurrentRoom == null) return;
            foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                CreateNewLabel(player.Value.NickName);
            }
            SetContentHeight();
        }

        void CreateNewLabel(string playerNickname)
        {
            TMPro.TextMeshProUGUI newPlayer = Instantiate(m_textPrefab, m_content);
            newPlayer.text = playerNickname;
            m_currentPlayerList.Add(newPlayer.gameObject);
        }

        void SetContentHeight()
        {
            float finalItemHeight = ((m_textHeight + m_spacing) * m_content.childCount) + m_topPadding;
            m_content.sizeDelta.Set(m_content.sizeDelta.x, finalItemHeight);
        }

        
    }
}