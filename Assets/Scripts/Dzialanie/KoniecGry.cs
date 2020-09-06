using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KoniecGry : MonoBehaviour {

    private float toNaliczenie = 5.0f;
    public string nazwaSceny;
    public string komunikat;
    //public string startNr;

    private GameObject canvasKomunikat;

    private Canvas canvas;

    // Use this for initialization
    void Start()
    {
        canvasKomunikat = GameObject.FindGameObjectWithTag("Komunikat");
        canvas = canvasKomunikat.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

        if (EnemyAI.iloscEnemy == 0)
        {
            toNaliczenie -= Time.deltaTime;
            if (toNaliczenie <= 0)
            {
                canvas.enabled = true;
                ustawKomunikat();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //PlayerInstancja.startNr = startNr;
                    canvas.enabled = false;
                    
                    Application.LoadLevel(nazwaSceny);
                    
                }
            }
        }
        else
        {
            canvas.enabled = false;
            ustawKomunikat();
        }


    }

    public void ustawKomunikat()
    {
        canvasKomunikat.GetComponent<Transform>().GetComponentInChildren<Text>().text = komunikat;
    }
}
