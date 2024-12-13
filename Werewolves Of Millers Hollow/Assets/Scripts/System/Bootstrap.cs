using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            GameSceneManager.Instance.LoadMenuScene();
        }
    }
}