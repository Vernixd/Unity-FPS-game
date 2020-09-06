using UnityEngine;
using System.Collections;

public class WodaTrigger : MonoBehaviour {


    private Transform trans;

    private Transform graczTransform;


    private GameObject woda;
    private PodzialKamery podzialKamery;

    private Transform kameraWWodzieTrans;
    private Transform kameraGlownaTrans;

    private Transform kameraBroni;
    private GameObject strzal;

    private PlayerControler graczKontroler;

    private bool graczWWodzie;
    public float poziomAktywacjiSkryptu = 0.6f;

    private bool przelaczSkrypt = false;
    private bool przelaczony;

    private AudioSource zrodloDzwieku;
    public AudioClip dzwiekPodWoda;
    public AudioClip dzwiekWody;

    // Use this for initialization
    void Start()
    {
        trans = GetComponent<Transform>();
        woda = trans.Find("Woda1").gameObject;
        kameraGlownaTrans = Camera.main.GetComponent<Transform>();
        podzialKamery = kameraGlownaTrans.GetComponent<PodzialKamery>();
        kameraWWodzieTrans = kameraGlownaTrans.Find("KameraWWodzie");
        kameraBroni = kameraGlownaTrans.Find("GunsCamera");
        zrodloDzwieku = trans.GetComponent<AudioSource>();
        DzwiekiWody();
        strzal = GameObject.FindGameObjectWithTag("Bron");

    }

    // Update is called once per frame
    void Update()
    {
        if (graczWWodzie)
        {

            if (przelaczSkrypt && !przelaczony)
            {
                wlaczPoruszanieWWodzie();
                przelaczony = true;

                DzwiekiPodWoda();

            }
            else if (!przelaczSkrypt && przelaczony)
            {
                wlaczPoruszaniePoLadzie();
                przelaczony = false;

                DzwiekiWody();
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        

        Transform obiektTrans = other.GetComponent<Transform>();
        graczKontroler = obiektTrans.GetComponent<PlayerControler>();

        if (graczKontroler != null)
        {
            graczTransform = other.GetComponent<Transform>();


            graczWWodzie = true;

            podzialKamery.setWoda(woda);
            kameraWWodzieTrans.gameObject.SetActive(true);
            kameraBroni.gameObject.SetActive(false);
            strzal.GetComponent<Shooting>().enabled = false;
        }


    }


    void OnTriggerExit(Collider other)
    {
      
        Transform obiektTrans = other.GetComponent<Transform>();
        graczKontroler = obiektTrans.GetComponent<PlayerControler>();
        if (graczKontroler != null)
        {
            graczWWodzie = false;
            podzialKamery.setWoda(null);

            kameraWWodzieTrans.gameObject.SetActive(false);
            kameraBroni.gameObject.SetActive(true);
            strzal.GetComponent<Shooting>().enabled = true;
        }

    }

    void OnTriggerStay(Collider other)
    {
        

        Transform graczTransform = other.GetComponent<Transform>();

        graczKontroler = graczTransform.GetComponent<PlayerControler>();

        if (graczKontroler != null)
        {
            float powierzchnia = trans.position.y + trans.localScale.y;

            if (graczTransform.position.y > powierzchnia - poziomAktywacjiSkryptu)
            {
                przelaczSkrypt = false;
            }
            else
            {
                przelaczSkrypt = true;
            }

           
        }

    }


    private void wlaczPoruszanieWWodzie()
    {
        if (graczTransform != null)
        {

            graczKontroler = graczTransform.GetComponent<PlayerControler>();

            if (graczKontroler != null)
            {
                graczKontroler.enabled = false;


                GraczWodaKontroler graczWodaKontroler = graczTransform.GetComponent<GraczWodaKontroler>();
                graczWodaKontroler.enabled = true;


                graczWodaKontroler.myszGoraDol = graczKontroler.myszGoraDol;
                graczWodaKontroler.aktualnaWysokoscSkoku = 0f;
            }
        }
    }


    private void wlaczPoruszaniePoLadzie()
    {
        if (graczTransform != null)
        {


            graczKontroler = graczTransform.GetComponent<PlayerControler>();
            if (graczKontroler != null)
            {
                graczKontroler.enabled = true;


                GraczWodaKontroler graczWodaKontroler = graczTransform.GetComponent<GraczWodaKontroler>();
                graczWodaKontroler.enabled = false;


                graczKontroler.aktualnaWysokoscSkoku = graczWodaKontroler.aktualnaWysokoscSkoku;
                graczKontroler.myszGoraDol = graczWodaKontroler.myszGoraDol;

            }
        }
    }


    private void DzwiekiPodWoda()
    {
        if (zrodloDzwieku != null && dzwiekPodWoda != null)
        {
            zrodloDzwieku.Stop();
            zrodloDzwieku.clip = dzwiekPodWoda;
            zrodloDzwieku.spatialBlend = 0;
            zrodloDzwieku.Play();
        }
    }

    private void DzwiekiWody()
    {
        if (zrodloDzwieku != null && dzwiekWody != null)
        {
            zrodloDzwieku.Stop();
            zrodloDzwieku.spatialBlend = 1;
            zrodloDzwieku.clip = dzwiekWody;
            zrodloDzwieku.Play();
        }
    }

}