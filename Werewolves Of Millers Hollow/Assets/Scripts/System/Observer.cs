namespace Game
{
    public static class MultiplayerObserver
    {
        public static event System.Action OnConnectToLobby = null;
        public static event System.Action OnTryConnectToLobby = null;
        public static event System.Action<string> OnFailedToConnectToLobby = null;

        public static event System.Action OnEnteringRoom = null;
        

        public static void ConnectToLobby() => OnConnectToLobby?.Invoke();
        public static void TryConnectToLobby() => OnTryConnectToLobby?.Invoke();
        public static void FailedToConnectToLobby(string failedMessage) => OnFailedToConnectToLobby?.Invoke(failedMessage);
        public static void EnteringRoom() => OnEnteringRoom?.Invoke();
    }
}