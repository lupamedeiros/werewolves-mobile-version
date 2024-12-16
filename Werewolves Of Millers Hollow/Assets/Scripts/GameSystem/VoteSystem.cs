using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteSystem : MonoBehaviour
{
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveButtons()
    {
        button1.SetActive(true);
        button2.SetActive(true);
    }

    public void DisableButtons()
    {
        button1.SetActive(false);
        button2.SetActive(false);
    }
}
