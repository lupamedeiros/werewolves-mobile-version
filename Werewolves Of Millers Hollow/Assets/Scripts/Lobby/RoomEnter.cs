using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lobby
{
    public class RoomEnter : MonoBehaviourPunCallbacks
    {
        [SerializeField] RoomItem m_roomItemPrefab;
        [SerializeField] float m_yButtonSize;
        List<RoomItem> m_roomItems = new();
        [SerializeField] RectTransform m_contentObject;

        private void Awake()
        {
            (m_roomItems ??= new()).Clear();
        }

        public void UpdateRoomList(List<RoomInfo> roomList)
        {
            foreach (RoomItem room in m_roomItems)
            {
                Destroy(room.gameObject);
            }
            m_roomItems.Clear();

            foreach (RoomInfo room in roomList)
            {
                RoomItem newRoom = Instantiate(m_roomItemPrefab, m_contentObject);
                newRoom.SetRoomName(room.Name);
                m_roomItems.Add(newRoom);
            }

            float x = m_contentObject.sizeDelta.x;
            float y = m_yButtonSize * m_roomItems.Count;
            m_contentObject.sizeDelta = new Vector2(x, y);
        }
    }
}