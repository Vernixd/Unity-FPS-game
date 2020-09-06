using UnityEngine;
using System.Collections;

public class Firecamp : MonoBehaviour {

    private bool isPlayer;
    public ParticleSystem Flame;
    public ParticleSystem Smoke;
    public Light FireLight;
    public AudioSource FireSound;
    public bool FireOn = false;
    public GUISkin skin;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isPlayer = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!FireOn)
                {
                    FireLight.enabled = true;
                    FireSound.enabled = true;
                    Flame.GetComponent<ParticleSystem>().enableEmission = true;
                    Smoke.GetComponent<ParticleSystem>().enableEmission = true;
                    FireOn = true;
                }
                else
                {
                    FireLight.enabled = false;
                    FireSound.enabled = false;
                    Flame.GetComponent<ParticleSystem>().enableEmission = false;
                    Smoke.GetComponent<ParticleSystem>().enableEmission = false;
                    FireOn = false;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isPlayer = false;
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;
        if (isPlayer)
        {
            if (!FireOn)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 200, 100), "Użyj aby rozpalić ognisko");
            }
           else
           {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 200, 100), "Uzyj aby zgasić ognisko");
            }
        }
    }


}
