using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameSceneManager : Singleton<GameSceneManager>
    {
        [Header("Scenes Index")]
        [SerializeField, Min(-1)] int m_menuSceneIndex;
        [SerializeField, Min(-1)] int m_lobbySceneIndex;
        [SerializeField, Min(-1)] int m_gameHubSceneIndex;
        [SerializeField, Min(-1)] int m_gameSceneIndex;
        public int GameSceneIndex => m_gameSceneIndex;
        public void LoadMenuScene()
        {
            LoadScene(m_menuSceneIndex);
        }

        public void LoadLobbyScene()
        {
            LoadScene(m_lobbySceneIndex);
        }

        public void LoadGameHubScene()
        {
            LoadScene(m_gameHubSceneIndex);
        }

        public void LoadGameScene()
        {
            LoadScene(m_gameSceneIndex);
        }

        void LoadScene(int index)
        {
            if (index < 0)
            {
                Debug.LogError("Cena não especificada");
                return;
            }
            SceneManager.LoadScene(index);
        }
    }
}