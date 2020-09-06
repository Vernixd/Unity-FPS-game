using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StaminaUI : MonoBehaviour {

	private PlayerControler playerControler;

	public Image stamina;
	public float poziomStaminy = 5;
	private float czasBiegania;

	// Use this for initialization
	void Start () {
		playerControler = GetComponent<PlayerControler>();
		czasBiegania = poziomStaminy;

        czyPasekStaminyZnaleziony();
    }
	
	// Update is called once per frame
	void Update () {
		aktualizacjaPaskaStaminy();
	}

	
	private void aktualizacjaPaskaStaminy(){
		if (playerControler.enabled && playerControler.czyGraczBiegnie() && playerControler.czyGraczChodzi() && czasBiegania > -0.5f) {
			czasBiegania -= Time.deltaTime;
		} else {
			if(czasBiegania < poziomStaminy) {
				czasBiegania += Time.deltaTime;
			}
		}
		if (stamina != null) {
			stamina.fillAmount = czasBiegania / poziomStaminy;
		}
		
	}

	
	public bool brakStaminy(){
		return czasBiegania <= 0;
	}

   
    private void czyPasekStaminyZnaleziony() {
        if (stamina == null) {
            GameObject hud = GameObject.FindGameObjectWithTag("HUD");
            Transform hudT = hud.GetComponent<Transform>();
            Transform staminaTLo = hudT.Find("StaminaTlo");
            if (staminaTLo != null) {
                Transform staminaTrans = staminaTLo.Find("Stamin");
                if(staminaTrans != null) {
                    stamina = staminaTrans.GetComponent<Image>();
                }

            }
             
        }
    }
}
