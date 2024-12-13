using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameRoom
{
    public class OnClickLeaveRoom : MonoBehaviourPunCallbacks
    {
        [SerializeField] Button m_button;
        bool m_leaving;
        public override void OnEnable()
        {
            base.OnEnable();
            m_leaving = false;
            m_button.onClick.AddListener(OnClickLeave);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            m_button.onClick.RemoveListener(OnClickLeave);
        }

        void OnClickLeave()
        {
            if (m_leaving) return;
            Debug.Log("SAINDO");
            m_leaving = true;
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            Debug.Log("SAIU");
            GameSceneManager.GetInstance(false)?.LoadLobbyScene();
        }
    }
}