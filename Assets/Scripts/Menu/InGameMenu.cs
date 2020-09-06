using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class InGameMenu : MonoBehaviour {

    private bool pauza = false;
    public float buttonWidth = 300;
    public float buttonHeight = 60;
    public Texture GameLogo;
    public GUISkin skin;


    private float buttonMargin = 10;

    private Transform gracz;
    private Transform bron1;
    private Transform bron2;
    private Transform bron3;

    // Use this for initialization
    void Start()
    {
        gracz = PlayerInstancja.instancja.GetComponent<Transform>();
        bron1 = Gun1Instancja.instancja.GetComponent<Transform>();
        if (bron2 != null)
        {
            bron2 = Gun2Instancja.instancja.GetComponent<Transform>();
        }
        if (bron3 != null)
        {
            bron3 = Gun3Instancja.instancja.GetComponent<Transform>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauza)
            {
                Time.timeScale = 1;
                pauza = false;
                GetComponent<AudioSource>().Stop();
            }
            else
            {
                Time.timeScale = 0;
                pauza = true;
                GetComponent<AudioSource>().Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            zapisz();
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            wczytaj();
        }
    }

    void OnGUI()
    {
        if (pauza)
        {
            Color background = Color.black;
            background.a = 0.3f;
            DrawQuad(new Rect(0, 0, Screen.width, Screen.height), background);

            GUI.DrawTexture(new Rect(0, 0, 700, 200), GameLogo);
            GUI.skin = skin;
            GUI.BeginGroup(new Rect(300, 300, buttonWidth, (buttonHeight + buttonMargin) * 5));

            //if (GUI.Button(new Rect(0, 0, buttonWidth, buttonHeight), "Nowa gra"))
            //{
            //    GetComponent<AudioSource>().Stop();
            //    Time.timeScale = 1;
            //    pauza = false;
            //    SceneManager.LoadScene("Game");
            //}
            if (GUI.Button(new Rect(0, (buttonHeight + buttonMargin), buttonWidth, buttonHeight), "Zapisz"))
            {
                zapisz();
            }
            if (GUI.Button(new Rect(0, (buttonHeight + buttonMargin) * 2, buttonWidth, buttonHeight), "Wczytaj"))
            {
                wczytaj();
               
            }
            if (GUI.Button(new Rect(0, (buttonHeight + buttonMargin) * 3, buttonWidth, buttonHeight), "Wyjście"))
            {
                Application.Quit();
            }
            GUI.EndGroup();
        }

    }

    void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }
    public void wczytaj()
    {
        Debug.Log("WCZYTAJ");


        if (File.Exists(Application.persistentDataPath + "/graczInfo.data"))
        {

            FileStream plik = File.Open(Application.persistentDataPath + "/graczInfo.data", FileMode.Open);


            BinaryFormatter bf = new BinaryFormatter();


            GraczDane dane = (GraczDane)bf.Deserialize(plik);
            plik.Close();

            Time.timeScale = 1;
            pauza = false;
            GetComponent<AudioSource>().Stop();
            PlayerInstancja.respown = false;
            SceneManager.LoadScene(dane.nazwaSceny);
            gracz.GetComponent<HPUI>().setZdrowie(dane.zdrowie);
            gracz.GetComponent<Transform>().position = dane.pozycja.pobierzWektor();
            gracz.GetComponent<Transform>().rotation = dane.obrot.pobierzQuaternion();
            bron1.GetComponent<Shooting>().setAmmo(dane.magazynek1,dane.amunicja1);
            bron2.GetComponent<Shooting>().setAmmo(dane.magazynek2, dane.amunicja2);
            bron3.GetComponent<Shooting>().setAmmo(dane.magazynek3, dane.amunicja3);
            
        }
    }

    public void zapisz()
    {
        Debug.Log("ZAPISZ");


        FileStream plik = File.Create(Application.persistentDataPath + "/graczInfo.data");


        GraczDane dane = new GraczDane();
        dane.nazwaSceny = Application.loadedLevelName;
        dane.zdrowie = gracz.GetComponent<HPUI>().currentHealth;
        dane.pozycja = new Vector3Serialization(gracz.GetComponent<Transform>().position);
        dane.obrot = new QuaternionSerialization(gracz.GetComponent<Transform>().rotation);
        dane.magazynek1 = bron1.GetComponent<Shooting>().currentClip;
        dane.amunicja1 = bron1.GetComponent<Shooting>().currentAmmo;
        dane.magazynek2 = bron1.GetComponent<Shooting>().currentClip;
        dane.amunicja2 = bron1.GetComponent<Shooting>().currentAmmo;
        dane.magazynek3 = bron1.GetComponent<Shooting>().currentClip;
        dane.amunicja3 = bron1.GetComponent<Shooting>().currentAmmo;

        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(plik, dane);
        plik.Close();
    }

}
[Serializable]
class GraczDane
{
    public String nazwaSceny;
    public float zdrowie;
    public Vector3Serialization pozycja;
    public QuaternionSerialization obrot;
    public int amunicja1;
    public int magazynek1;
    public int amunicja2;
    public int magazynek2;
    public int amunicja3;
    public int magazynek3;
}


[Serializable]
class Vector3Serialization
{


    public float x;
    public float y;
    public float z;


    public Vector3Serialization(Vector3 v)
    {
        ustawWektor(v);
    }


    public void ustawWektor(Vector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }


    public Vector3 pobierzWektor()
    {
        Vector3 vec = new Vector3();
        vec.x = this.x;
        vec.y = this.y;
        vec.z = this.z;
        return vec;
    }

}


[Serializable]
class QuaternionSerialization
{


    public float x;
    public float y;
    public float z;
    public float w;


    public QuaternionSerialization(Quaternion v)
    {
        ustawQuaternion(v);
    }

    public void ustawQuaternion(Quaternion v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
        this.w = v.w;
    }

    public Quaternion pobierzQuaternion()
    {
        Quaternion vec = new Quaternion();
        vec.x = this.x;
        vec.y = this.y;
        vec.z = this.z;
        vec.w = this.w;
        return vec;
    }

}