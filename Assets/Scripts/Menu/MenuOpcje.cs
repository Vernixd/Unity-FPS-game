using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuOpcje : MonoBehaviour
{
    public Texture GameLogo;

    public float jasnosc; 
    public UnityEngine.UI.Slider jasnoscSlider;

    public float glosnosc;
    public UnityEngine.UI.Slider glosnoscSlider;

    public int poziomAliasingu;
    public GameObject wylaczony;
    public GameObject x2;
    public GameObject x4;
    public GameObject x8;


    public void aliasing (int dzialanie)
    {
        if (dzialanie == 2 && poziomAliasingu < 4)
        {
            poziomAliasingu++;
        }
        if (dzialanie == 1 && poziomAliasingu > 1)
        {
            poziomAliasingu--;
        }
    }

    // Use this for initialization
    void Start () {
        Cursor.visible = true;


    }
	
	// Update is called once per frame
	void Update ()
    {
        jasnosc = Mathf.Round(jasnoscSlider.value * 100) / 100;
        RenderSettings.ambientLight = new Color(jasnosc, jasnosc, jasnosc, 1);

        glosnosc = Mathf.Round(glosnoscSlider.value * 100) / 100;
        AudioListener.volume = glosnosc;

        if (poziomAliasingu == 1)
        {
            QualitySettings.antiAliasing = 1;
            wylaczony.gameObject.SetActive(true);
            x2.gameObject.SetActive(false);
            x4.gameObject.SetActive(false);
            x8.gameObject.SetActive(false);
        }
        else if (poziomAliasingu == 2)
        {
            QualitySettings.antiAliasing = 2;
            wylaczony.gameObject.SetActive(false);
            x2.gameObject.SetActive(true);
            x4.gameObject.SetActive(false);
            x8.gameObject.SetActive(false);
        }
        else if (poziomAliasingu == 3)
        {
            QualitySettings.antiAliasing = 4;
            wylaczony.gameObject.SetActive(false);
            x2.gameObject.SetActive(false);
            x4.gameObject.SetActive(true);
            x8.gameObject.SetActive(false);
        }
        else if (poziomAliasingu == 4)
        {
            QualitySettings.antiAliasing = 8;
            wylaczony.gameObject.SetActive(false);
            x2.gameObject.SetActive(false);
            x4.gameObject.SetActive(false);
            x8.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Błąd! Nie ma takiego Aliasingu!");
            poziomAliasingu = 1;
        }
    }

    public void Zapisz()
    {
        PlayerPrefs.SetFloat("jasnosc", jasnosc);
        PlayerPrefs.SetFloat("glosnosc", glosnosc);
        PlayerPrefs.SetInt("poziomAliasingu", poziomAliasingu);
        SceneManager.LoadScene("Menu");
    }

    public void Wstecz()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Wczytaj()
    {
        jasnosc = PlayerPrefs.GetFloat("jasnosc");
        jasnoscSlider.value = jasnosc;
        glosnosc = PlayerPrefs.GetFloat("glosnosc");
        glosnoscSlider.value = glosnosc;
        poziomAliasingu = PlayerPrefs.GetInt("poziomAliasingu");
        
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 700, 200), GameLogo);
       
    }

   void Awake()
    {
        Wczytaj();
    }
}

