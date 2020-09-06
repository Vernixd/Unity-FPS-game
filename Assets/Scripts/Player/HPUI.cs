using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPUI : MonoBehaviour {

    private float maxHealth = 300;
    public float currentHealth;

    private float canHeal = 0.0f;
    private float canRegenerate = 0.0f;


    public Texture2D bloodTexture;
    private bool hit = false;
    private float opacity = 0.0f;

    public Light torch;
    bool torchOn = false;

    public Image hp;

    // Use this for initialization
    void Start ()
    {
        currentHealth = maxHealth;

        czyPasekHPZnaleziony();
    }
	
	// Update is called once per frame
	void Update ()
    {
        aktualizacjaPaskaHP();

        if (Input.GetKeyDown(KeyCode.P))
        {
            takeHit(30);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!torchOn)
            {
                torch.enabled = true;
                torchOn = true;
            }
            else
            {
                torch.enabled = false;
                torchOn = false;
            }
        }

        if (canHeal > 0.0f)
        {
            canHeal -= Time.deltaTime;
        }
        if (canRegenerate > 0.0f)
        {
            canRegenerate -= Time.deltaTime;
        }

        //if (canHeal <= 0.0f && currentHealth < maxHealth)
        //{
        //    regenerate(ref currentHealth, maxHealth);
        //}
    }

    void takeHit(float damage)
    {
        hit = true;
        opacity = 1.0f;

        if (currentHealth < 1)
        {
            SceneManager.LoadScene("Śmierć");
            Destroy(GameObject.Find("Player"));
            Destroy(GameObject.Find("InterfaceStaly"));
                }

        if (opacity <= 0)
        {
            hit = false;
        }
        else
        {
            currentHealth -= damage;
        }

        //if (currentHealth < maxHealth)
        //{
        //    canHeal = 5.0f;
        //}
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    //void regenerate(ref float currentStat, float maxStat)
    //{
    //    currentStat += maxStat * 0.003f;
    //    Mathf.Clamp(currentStat, 0, maxStat);
    //}

    IEnumerator waitAndChangeOpacity()
    {
        yield return new WaitForEndOfFrame();
        opacity -= 0.05f;
    }

    private void aktualizacjaPaskaHP()
    {
        if (hp != null)
        {
            hp.fillAmount = currentHealth / maxHealth;
        }

    }

    public void setZdrowie(float zdrowie)
    {
        currentHealth = zdrowie;
    }

    void OnGUI()
    {
        if (hit)
        {
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, opacity);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bloodTexture, ScaleMode.ScaleToFit);
            StartCoroutine("waitAndChangeOpacity");
        }
    }

    private void czyPasekHPZnaleziony()
    {
        if (hp == null)
        {
            GameObject hud = GameObject.FindGameObjectWithTag("HUD");
            Transform hudT = hud.GetComponent<Transform>();
            Transform hpTlo = hudT.Find("HpTlo");
            if (hpTlo != null)
            {
                Transform hpTrans = hpTlo.Find("HP");
                if (hpTrans != null)
                {
                   hp = hpTrans.GetComponent<Image>();
                }

            }

        }
    }
}
