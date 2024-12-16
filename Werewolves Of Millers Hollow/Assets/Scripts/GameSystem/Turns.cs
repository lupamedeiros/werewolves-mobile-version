using static Game.Turn.DayLogic;
using UnityEngine;

namespace Game.Turn
{
    [System.Serializable]
    public class DayLogic : ITurnLogic
    {
        [SerializeField, Min(0)] float m_dayDurationSec = 90;
        float m_endDayTime;

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
        [SerializeField, Min(0)] float m_nightDurationSec = 90;
        float m_endNightTime;

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