using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
    }

    public void startGame()
    {
        Application.LoadLevel("Game");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    
}
