using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game.Multiplayer;

namespace Game.Turn
{
    public class TurnShifter : MonoBehaviour
    {
        [SerializeField] Turn m_startTurn = Turn.Night;
        [field: SerializeField] public DayLogic m_daySettings { get; private set; } = new();
        [field: SerializeField] public NightLogic m_nightSettings { get; private set; } = new();
        Dictionary<Turn, ITurnLogic> m_logicByEnum;

        public Turn CurrentTurn => PropertiesHandler.GetRoomPropertyValue<Turn>(PropertiesHandler.PROP_ROOM_SHIFT);

        ITurnLogic GetTurnLogic()
        {
            Turn currentTurn = PropertiesHandler.GetRoomPropertyValue<Turn>(PropertiesHandler.PROP_ROOM_SHIFT);
            return m_logicByEnum[currentTurn];
        }

        void SetTurnLogic(Turn newTurn)
        {
            PropertiesHandler.SetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFT, newTurn);
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
            UpdateTurn();
        }

        void UpdateTurn()
        {
            Debug.Log($"O turno atual é: {CurrentTurn}");
            if (!PhotonNetwork.IsMasterClient) return;
            GetTurnLogic()?.UpdateTurn();
        }

        void SwitchTurn(Turn newTurn)
        {
            if (!PhotonNetwork.IsMasterClient) return;
            GetTurnLogic()?.EndTurn();
            
            Debug.Log($"Turno {CurrentTurn} terminou!");
            
            SetTurnLogic(newTurn);

            GetTurnLogic().StartTurn();

            Debug.Log($"Turno {CurrentTurn} começou!");
        }

        void SetDictionary()
        {
            m_logicByEnum = new Dictionary<Turn, ITurnLogic>()
            {
                { Turn.None, null },
                { Turn.Day, m_daySettings },
                { Turn.Night, m_nightSettings }
            };
        }
        void SetupTurn()
        {
            SetDictionary();
            if (!PhotonNetwork.IsMasterClient) return;
            SwitchTurn(m_startTurn);
        }
        public void SwitchToDay() => SwitchTurn(Turn.Day);
        public void SwitchToNight() => SwitchTurn(Turn.Night);
    }    
}