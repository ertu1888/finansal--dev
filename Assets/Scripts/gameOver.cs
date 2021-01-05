using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOver : MonoBehaviour
{
    public int score;
    public int highScore = 0;

    public Text loadScore;
    public Text loadHighScore;

    void Start()
    {
        showScore();
    }

   
    void Update()
    {
        
    }

    public void showScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            if (Application.loadedLevel == 1)
            {
                PlayerPrefs.DeleteKey("Score");
                score = 0;
            }
            else
            {
                score = PlayerPrefs.GetInt("Score");
                loadScore.text = score.ToString();
            }
        }

        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (Application.loadedLevel == 2)
            {
                highScore = PlayerPrefs.GetInt("HighScore");
                loadHighScore.text = highScore.ToString();
            }
            
        }
    }

    public void newGame()
    {
        Application.LoadLevel("Game");
    }

    public void mainMenu()
    {
        Application.LoadLevel("Menu");
    }
}
