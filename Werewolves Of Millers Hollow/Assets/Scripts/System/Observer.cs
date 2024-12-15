using Photon.Realtime;
using System.Collections.Generic;

namespace Game
{
    public static class MultiplayerObserver
    {
        public static event System.Action OnConnectToMaster = null;
        public static event System.Action OnConnectToLobby = null;
        public static event System.Action OnTryConnectToLobby = null;
        public static event System.Action<string> OnFailedToConnectToLobby = null;

        public static event System.Action OnEnteringRoom = null;
        
        public static event System.Action<List<RoomInfo>> OnUpdateRoomList = null;

        public static void ConnectToMaster() => OnConnectToMaster?.Invoke();
        public static void ConnectToLobby() => OnConnectToLobby?.Invoke();
        public static void TryConnectToLobby() => OnTryConnectToLobby?.Invoke();
        public static void FailedToConnectToLobby(string failedMessage) => OnFailedToConnectToLobby?.Invoke(failedMessage);
        public static void EnteringRoom() => OnEnteringRoom?.Invoke();
        public static void UpdateRoomList(List<RoomInfo> roomList) => OnUpdateRoomList?.Invoke(roomList);


    }

    public static class GameSystemObserver
    {
        public static event System.Action OnDayStart = null;
        public static event System.Action OnDayEnd = null;

        public static event System.Action OnNightStart = null;
        public static event System.Action OnNightEnd = null;

        public static void StartDay() => OnDayStart?.Invoke();
        public static void EndDay() => OnDayEnd?.Invoke();
        public static void StartNight() => OnNightStart?.Invoke();
        public static void EndNight() => OnNightEnd?.Invoke();
    }
}