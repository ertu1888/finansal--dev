using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butonBas : MonoBehaviour
{

    UfoScript ufo;

    // Start is called before the first frame update
    public void Start()
    {
        ufo = GetComponent<UfoScript>();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void yukari()
    {
        ufo.yukari = true;
    }

    public void asagi()
    {
        ufo.yukari = false;
    }
}
