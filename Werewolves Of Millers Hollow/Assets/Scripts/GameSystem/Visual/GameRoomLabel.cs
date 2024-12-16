using Game.Character;
using Game.Multiplayer;
using Game.Turn;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameRoom
{
    public class GameRoomLabel : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI m_nicknameTxt;
        [SerializeField] TMPro.TextMeshProUGUI m_abilityTxt;
        [SerializeField] TMPro.TextMeshProUGUI m_shiftTxt;
        [SerializeField] UnityEngine.UI.Image m_clock;
        [SerializeField] Division m_division;
        private void Awake()
        {
            
        }

        void Update()
        {
            HandleText();
            HandleClock();
        }

        void HandleClock()
        {
            m_clock.fillAmount = Time.time/PropertiesHandler.GetRoomPropertyValue<float>(PropertiesHandler.PROP_ROOM_SHIFTTIME);
            m_shiftTxt.text = $"{PropertiesHandler.GetRoomPropertyValue<Turn.Turn>(PropertiesHandler.PROP_ROOM_SHIFT)}";
        }

        void HandleText()
        {
            m_nicknameTxt.text = PhotonNetwork.LocalPlayer.NickName;
            m_abilityTxt.text = m_division.m_playerWithAbility[PhotonNetwork.LocalPlayer].name;
        }
    }
}