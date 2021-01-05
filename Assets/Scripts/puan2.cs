﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puan2 : MonoBehaviour
{
    public float hiz;
    public bool aktif;
    public GameObject objeler;
    public float sayac;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-((hiz + sayac) * Time.deltaTime), 0, 0);

        if (aktif)
        {
            objeler.SetActive(false);

            aktif = false;
            Invoke("trueYap", 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D nesne)
    {
        if (nesne.gameObject.tag == "KarakterOlustur")
        {
            //nesne.gameObject.transform.root.gameObject.GetComponent<puan1>().aktif = true;
            Invoke("puanOlustur", 2);
            //sayac = sayac + 0.5f;
        }
    }

    // gameobject e değince döngü halinde puanları oluşturmak için 
    public void puanOlustur()
    {
        objeler.transform.localPosition = new Vector2(13, Random.Range(3.8f, -4.5f));
    }

    public void trueYap()
    {
        objeler.transform.localPosition = new Vector2(-6, 0); // ufoya deydikten sonra ekran dışına taşı 
        objeler.SetActive(true);
    }
}
