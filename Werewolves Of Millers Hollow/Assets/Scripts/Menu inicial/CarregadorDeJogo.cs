using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregadorDeJogo : MonoBehaviour
{
    public void CarregarJogo()
    {
        SceneManager.LoadScene(1);
    }

    public void FecharJogo()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        
        Application.Quit();
        
    }
}
