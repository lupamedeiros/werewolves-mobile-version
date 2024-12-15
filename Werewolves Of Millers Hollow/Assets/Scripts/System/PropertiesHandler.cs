using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Multiplayer
{
    public static class PropertiesHandler
    {
        public const string PROP_ROOM_MINPLAYERCOUNT = "mP";
        public const string PROP_PLAYER_STATE = "st";
        public const string PROP_PLAYER_ABILITY = "ab";

        public static T GetRoomPropertyValue<T>(string propName)
        {
            if (string.IsNullOrEmpty(propName)) return default;

            Room currentRoom = PhotonNetwork.CurrentRoom;
            if (currentRoom == null) return default;
            if (currentRoom.CustomProperties == null || currentRoom.CustomProperties.Count <= 0) return default;
            if (!currentRoom.CustomProperties.ContainsKey(propName)) return default;

            if (currentRoom.CustomProperties[propName] is T result)
            {
                return result;
            }
            Debug.LogError($"Não é possível converter o valor da propriedade {propName} em {typeof(T)}");
            return default;
        }
        public static T GetPlayerPropertyValue<T>(Player player, string propName)
        {
            if (player == null) return default;
            if (string.IsNullOrEmpty(propName)) return default;
            if (player.CustomProperties == null || player.CustomProperties.Count <= 0) return default;
            if (!player.CustomProperties.ContainsKey(propName)) return default;

            if (player.CustomProperties[propName] is T result)
            {
                return result;
            }
            Debug.LogError($"Não é possível converter o valor da propriedade {propName} em {typeof(T)}");
            return default;
        }

        public static bool PlayersPropsSet()
        {
            if (!PhotonNetwork.IsConnected) return false;
            if (!PhotonNetwork.InRoom) return false;
            if (PhotonNetwork.CurrentRoom.PlayerCount <= 0) return false;
            foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                if (player.Value.CustomProperties == null || player.Value.CustomProperties.Count <= 0) return false;
            }

            return true;
        }
    }
}