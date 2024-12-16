using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Turn
{
    public class TurnShifter : MonoBehaviour
    {
        [SerializeField] Turn m_startTurn = Turn.Night;
        [SerializeField] DayLogic m_daySettings = new();
        [SerializeField] NightLogic m_nightSettings = new();
        Dictionary<Turn, ITurnLogic> m_logicByEnum;
        ITurnLogic m_currentTurnLogic;
        public Turn CurrentTurn
        {
            get
            {
                if (!m_logicByEnum.ContainsValue(m_currentTurnLogic)) return Turn.None;
                return m_logicByEnum.FirstOrDefault(x => x.Value == m_currentTurnLogic).Key;
            }
        }

        private void OnEnable()
        {
            GameSystemObserver.OnDayEnd += SwitchToNight;
            GameSystemObserver.OnNightEnd += SwitchToDay;
        }

        private void OnDisable()
        {
            GameSystemObserver.OnDayEnd -= SwitchToNight;
            GameSystemObserver.OnNightEnd -= SwitchToDay;
        }

        private void Awake()
        {
            SetupTurn();
        }

        private void Update()
        {
            Debug.Log($"O turno atual é: {CurrentTurn}");
            m_currentTurnLogic?.UpdateTurn();
        }

        void SwitchTurn(Turn newTurn)
        {
            m_currentTurnLogic?.EndTurn();
            Debug.Log($"Turno {CurrentTurn} terminou!");
            if (m_logicByEnum.TryGetValue(newTurn, out ITurnLogic newLogic))
            {
                m_currentTurnLogic = newLogic;
                m_currentTurnLogic.StartTurn();
            }
            else
            {
                m_currentTurnLogic = null;
            }
            Debug.Log($"Turno {CurrentTurn} começou!");
        }

        void SetDictionary()
        {
            m_logicByEnum = new Dictionary<Turn, ITurnLogic>()
            {
                { Turn.Day, m_daySettings},
                { Turn.Night, m_nightSettings}
            };
        }
        void SetupTurn()
        {
            SetDictionary();
            SwitchTurn(m_startTurn);
        }


        public void SwitchToDay() => SwitchTurn(Turn.Day);
        public void SwitchToNight() => SwitchTurn(Turn.Night);
    }    
}