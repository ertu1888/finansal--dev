using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class UfoScript : MonoBehaviour
{
    public static UfoScript instance;

    private gameOver go;

    public float ziplamaHiz;
    public bool yukari;

    // can
    public int can;
    public int maxCan;
    public GameObject[] canlar;

    // puan
    public Text puan;
    public int Puan;
    public int kirmiziPuan;
    public int maviPuan;
    public int yesilPuan;

    // audio
    public AudioSource audio;
    public AudioClip pointAudio;
    public AudioClip enemyAudio;
    public AudioClip heartAudio;

    public GameObject Panel;
    bool isPaused = false;

    public int extraCanKontrol;

    // admob
    public string rewardedID = "";
    RewardBasedVideoAd rAd;

    public GameObject extraHeartButton;
    public GameObject networkControlText;

    Rigidbody2D agirlik;

    public void Start()
    {
        agirlik = GetComponent<Rigidbody2D>();
        go = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameOver>();

        can = maxCan;
        canSistemi();

        isPaused = false;
        Time.timeScale = 1;
        extraCanKontrol = 0;
        Panel.SetActive(false);
        
        rAd = RewardBasedVideoAd.Instance;

        rAd.OnAdRewarded += VideoRewarded;
        rAd.OnAdClosed += VideoClosed;
        rAd.OnAdFailedToLoad += AdFailControl;

        this.RequestAds();

        networkControlText.SetActive(false);
    }

    public void Update()
    {
        
        go.score = kirmiziPuan + maviPuan + yesilPuan;
        puan.text = go.score.ToString();

        UpChar();
    }

    public void UpChar()
    {
        if (yukari)
        {
            agirlik.AddForce(Vector2.up * ziplamaHiz);
        }
    }

    public void OnTriggerEnter2D(Collider2D nesne)
    {
        if (nesne.gameObject.tag == "puan1")
        {
            nesne.gameObject.transform.root.gameObject.GetComponent<puan1>().aktif = true;
            kirmiziPuan = kirmiziPuan + 5;
            audio.PlayOneShot(pointAudio);
        }
        else if (nesne.gameObject.tag == "puan2")
        {
            nesne.gameObject.transform.root.gameObject.GetComponent<puan2>().aktif = true;
            yesilPuan = yesilPuan + 15;
            audio.PlayOneShot(pointAudio);
        }
        else if (nesne.gameObject.tag == "puan3")
        {
            nesne.gameObject.transform.root.gameObject.GetComponent<puan3>().aktif = true;
            maviPuan = maviPuan + 10;
            audio.PlayOneShot(pointAudio);
        }
        else if (nesne.gameObject.tag == "dusman")
        {
            nesne.gameObject.transform.root.gameObject.GetComponent<Dusman>().aktif = true;
            can--;
            canSistemi();
            audio.PlayOneShot(enemyAudio);
            if (can == 0)
            {
                gameOver();
            }
        }
        else if (nesne.gameObject.tag == "dusman2")
        {
            nesne.gameObject.transform.root.gameObject.GetComponent<Dusman2>().aktif = true;
            can--;
            canSistemi();
            audio.PlayOneShot(enemyAudio);
            if (can == 0)
            {
                gameOver();
            }
        }
        else if (nesne.gameObject.tag == "can")
        {
            nesne.gameObject.transform.root.gameObject.GetComponent<can>().aktif = true;
            can++;
            if (can > maxCan)
            {
                can = maxCan;
            }
            canSistemi();
            audio.PlayOneShot(heartAudio);
        }
        else if (nesne.gameObject.tag == "can1")
        {
            nesne.gameObject.transform.root.gameObject.GetComponent<can1>().aktif = true;
            can++;
            if (can > maxCan)
            {
                can = maxCan;
            }
            canSistemi();
            audio.PlayOneShot(heartAudio);
        }
    }

    void gameOver()
    {
        if (extraCanKontrol == 1)
        {
            showGameOver();
        }
        else
        {
            pauseGame();
            Debug.Log("oyun durdu");
            Panel.SetActive(true);
        }
        
        //Application.LoadLevel("Game Over");
    }

    void canSistemi()
    {
        for (int i = 0; i < maxCan; i++)
        {
            canlar[i].SetActive(false);
        }

        for (int i = 0; i < can; i++)
        {
            canlar[i].SetActive(true);
        }
    }

    void saveScore()
    {
        PlayerPrefs.SetInt("Score", go.score);
    }

    public void showGameOver()
    {
        saveScore();
        //pauseGame();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (PlayerPrefs.GetInt("HighScore") < go.score)
            {
                PlayerPrefs.SetInt("HighScore", go.score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", go.score);
        }
        Panel.SetActive(false);
        SceneManager.LoadScene(2);
    }

    void pauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            Debug.Log("1" +Time.timeScale);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            Debug.Log(Time.timeScale);
        }
    }

    //public void rasumeGame()
    //{
    //    extraCanKontrol++;
    //    if (extraCanKontrol == 1)
    //    {
    //        can++;
    //        canSistemi();
    //        Panel.SetActive(false);
    //        pauseGame();
    //        Debug.Log("oyun basladi");
    //    }
     
    //}

    private void RequestAds()
    {
        AdRequest request = new AdRequest.Builder().Build();

        this.rAd.LoadAd(request, rewardedID);
    }

    private void VideoRewarded(object sender, EventArgs e)
    {
        Reward();
    }

    private void VideoClosed(object sender, EventArgs e)
    {
        RequestAds();
    }

    public void showAds()
    {
        this.rAd.Show();
    }

    public void AdFailControl(object sender, AdFailedToLoadEventArgs e)
    {
        extraHeartButton.GetComponent<Button>().interactable = false;
        networkControlText.SetActive(true);
    }

    public void Reward()
    {
        extraCanKontrol++;
        if (extraCanKontrol == 1)
        {
            can++;
            canSistemi();
            Panel.SetActive(false);
            pauseGame();
            Debug.Log("oyun basladi");
        }
        //int can = PlayerPrefs.GetInt("can");
        //can++;
        //PlayerPrefs.SetInt("can" , can);
        //canSistemi();
    }
    
}
