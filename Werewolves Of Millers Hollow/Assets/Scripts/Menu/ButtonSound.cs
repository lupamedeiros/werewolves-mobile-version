using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSourceMenu; 
    public AudioSource audioSourceLobby;
    
        public void PlaySoundMenu()
        {
            if (audioSourceMenu != null)
            {
                audioSourceMenu.Play(); 
            }
        }
        
        public void PlaySoundLobby()
        {
            if (audioSourceLobby != null)
            {
                audioSourceLobby.Play(); 
            }
        }
}
