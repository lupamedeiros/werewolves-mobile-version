using static Game.Turn.DayLogic;
using UnityEngine;
using Game.Multiplayer;

namespace Game.Turn
{
    [System.Serializable]
    public class DayLogic : ITurnLogic
    {
        [field: SerializeField, Min(0)] public float m_dayDurationSec { get; private set; } = 90;
        float m_endDayTime
        {
            get => PropertiesHandler.GetRoomPropertyValue<float>(PropertiesHandler.PROP_ROOM_SHIFTTIME);
            set => PropertiesHandler.SetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFTTIME, value);
        }


        public void StartTurn()
        {
            m_endDayTime = Time.time + m_dayDurationSec;
            GameSystemObserver.StartDay();
        }

        public void UpdateTurn()
        {
            if (Time.time >= m_endDayTime)
            {
                EndTurn();
                GameSystemObserver.EndDay();
            }
        }

        public void EndTurn()
        {
            
        }  
    }

    [System.Serializable]
    public class NightLogic : ITurnLogic
    {
        [field: SerializeField, Min(0)] public float m_nightDurationSec { get; private set; } = 90;
        float m_endNightTime
        {
            get => PropertiesHandler.GetRoomPropertyValue<float>(PropertiesHandler.PROP_ROOM_SHIFTTIME);
            set => PropertiesHandler.SetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFTTIME, value);
        }

        public void StartTurn()
        {
            m_endNightTime = Time.time + m_nightDurationSec;
            GameSystemObserver.StartNight();
        }

        public void UpdateTurn()
        {
            if (Time.time >= m_endNightTime)
            {
                EndTurn();
                GameSystemObserver.EndNight();
            }
        }

        public void EndTurn()
        {
            
        }
    }

    public interface ITurnLogic
    {
        public void StartTurn();
        public void UpdateTurn();
        public void EndTurn();
    }

    public enum Turn
    {
        None = 0,
        Day = 1,
        Night = 2
    }
}