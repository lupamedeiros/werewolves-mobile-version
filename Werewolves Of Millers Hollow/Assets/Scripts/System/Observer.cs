namespace Game
{
    public static class MultiplayerObserver
    {
        public static System.Action OnConnectToLobby = null;
        public static System.Action OnTryConnectToLobby = null;
        public static System.Action<string> OnFailedToConnectToLobby = null;

        public static void ConnectToLobby() => OnConnectToLobby?.Invoke();
        public static void TryConnectToLobby() => OnTryConnectToLobby?.Invoke();
        public static void FailedToConnectToLobby(string failedMessage) => OnFailedToConnectToLobby?.Invoke(failedMessage);
    }
}