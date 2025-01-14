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
            object currentTime = PropertiesHandler.GetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFTTIME);
            object duration = PropertiesHandler.GetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFTDURATION);
            float fillAmount = (System.Convert.ToSingle(currentTime)) / (System.Convert.ToSingle(duration));
            //Debug.Log($"CurrentTime {currentTime:F2} Duration {duration:F2} {fillAmount}");
            m_clock.fillAmount = fillAmount;
            
        }

        void HandleText()
        {
            m_shiftTxt.text = $"{PropertiesHandler.GetRoomPropertyValue<Turn.Turn>(PropertiesHandler.PROP_ROOM_SHIFT)}";
            m_nicknameTxt.text = PhotonNetwork.LocalPlayer.NickName;
            m_abilityTxt.text = PropertiesHandler.GetPlayerPropertyValue<string>(PhotonNetwork.LocalPlayer, PropertiesHandler.PROP_PLAYER_ABILITY);
        }
    }
}