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
        [SerializeField, Min(-1)] int m_gameHubIndex;

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
            LoadScene(m_gameHubIndex);
        }

        void LoadScene(int index)
        {
            if (index < 0)
            {
                Debug.LogError("Cena n�o especificada");
                return;
            }
            SceneManager.LoadScene(index);
        }
    }
}