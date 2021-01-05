using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class can1 : MonoBehaviour
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
            Invoke("canOlustur", 8);
            //sayac = sayac + 0.5f;
        }
    }

    public void canOlustur()
    {
        objeler.transform.localPosition = new Vector2(15, Random.Range(3.8f, 0.5f));
    }

    public void trueYap()
    {
        objeler.transform.localPosition = new Vector2(-6, 0);
        objeler.SetActive(true);
    }
}
