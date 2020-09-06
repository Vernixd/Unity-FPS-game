using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextLvl : MonoBehaviour {

    private float toNaliczenie = 3.0f;
    public string nazwaSceny;
    public string komunikat;
    public string startNr;

    private GameObject canvasKomunikat;

    private Canvas canvas;

    // Use this for initialization
    void Start ()
    {
        canvasKomunikat = GameObject.FindGameObjectWithTag("Komunikat");
        canvas = canvasKomunikat.GetComponent<Canvas>();
    }
	
	// Update is called once per frame
	void Update () {

        if (EnemyAI.iloscEnemy == 0)
        {
            toNaliczenie -= Time.deltaTime;
            if (toNaliczenie <= 0)
            {
		 Application.LoadLevel(nazwaSceny);
                //canvas.enabled = true;
                //ustawKomunikat();
                //if (Input.GetKeyDown(KeyCode.E))
                //{
                  //  PlayerInstancja.startNr = startNr;
                   // Application.LoadLevel(nazwaSceny);
                //}               
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
