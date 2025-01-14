using static Game.Turn.DayLogic;
using UnityEngine;
using Game.Multiplayer;

namespace Game.Turn
{
    [System.Serializable]
    public class DayLogic : ITurnLogic
    {
        [field: SerializeField, Min(0)] public float m_dayDurationSec { get; private set; } = 90;
        float m_currentTime = 0;

        public void StartTurn()
        {
            m_currentTime = 0;
            PropertiesHandler.SetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFTTIME, 0);
            PropertiesHandler.SetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFTDURATION, m_dayDurationSec);
            GameSystemObserver.StartDay();
        }

        public void UpdateTurn()
        {
            //if (m_currentTime <= 0)
            //{
            //    float serverTime = PropertiesHandler.GetRoomPropertyValue<float>(PropertiesHandler.PROP_ROOM_SHIFTTIME);
            //    if (serverTime > 0)
            //    {
            //        m_currentTime = serverTime;
            //    }
            //}

            m_currentTime += Time.deltaTime;
            PropertiesHandler.SetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFTTIME, m_currentTime);
            float duration = PropertiesHandler.GetRoomPropertyValue<float>(PropertiesHandler.PROP_ROOM_SHIFTDURATION);
            if (m_currentTime >= duration)
            {
                EndTurn();
                GameSystemObserver.EndDay();
            }
        }

        public void EndTurn()
        {
            PropertiesHandler.SetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFTTIME, 0);
        }  
    }

    [System.Serializable]
    public class NightLogic : ITurnLogic
    {
        [field: SerializeField, Min(0)] public float m_nightDurationSec { get; private set; } = 90;
        float m_currentTime = 0;

        public void StartTurn()
        {
            m_currentTime = 0;
            PropertiesHandler.SetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFTTIME, 0);
            PropertiesHandler.SetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFTDURATION, m_nightDurationSec);
            GameSystemObserver.StartNight();
        }

        public void UpdateTurn()
        {
            //if (m_currentTime <= 0)
            //{
            //    float serverTime = PropertiesHandler.GetRoomPropertyValue<float>(PropertiesHandler.PROP_ROOM_SHIFTTIME);
            //    if (serverTime > 0)
            //    {
            //        m_currentTime = serverTime;
            //    }
            //}

            m_currentTime += Time.deltaTime;
            PropertiesHandler.SetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFTTIME, m_currentTime);
            float duration = PropertiesHandler.GetRoomPropertyValue<float>(PropertiesHandler.PROP_ROOM_SHIFTDURATION);
            if (m_currentTime >= duration)
            {
                EndTurn();
                GameSystemObserver.EndNight();
            }
        }

        public void EndTurn()
        {
            PropertiesHandler.SetRoomPropertyValue(PropertiesHandler.PROP_ROOM_SHIFTTIME, 0);
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