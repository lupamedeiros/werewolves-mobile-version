using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSourceMenu; 
    public AudioSource audioSourceLobby;
    public AudioSource audioSourceLobbyIn_De_crease;
    
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
        
        public void PlaySoundLobbyCreases()
        {
            if (audioSourceLobbyIn_De_crease != null)
            {
                audioSourceLobbyIn_De_crease.Play(); 
            }
        }
}
