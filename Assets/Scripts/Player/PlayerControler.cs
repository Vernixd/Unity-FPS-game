using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {


   
    public CharacterController characterControler;
    private StaminaUI stamina;

    public float akualnaPredkosc = 0f;
    public float predkoscPoruszania = 5.0f;
    public float predkoscBiegania = 10.0f;
    public float wysokoscSkoku = 7.0f;
    public float aktualnaWysokoscSkoku = 0f;

    public float czuloscMyszki = 3.0f;
    public float myszGoraDol = 0.0f;
    public float zakresMyszyGoraDol = 90.0f;

    
    private float rochPrzodTyl;
    
    private float rochLewoPrawo;
    
    public bool czyBiegnie;

    // Use this for initialization
    void Start()
    {
        stamina = GetComponent<StaminaUI>();
        characterControler = GetComponent<CharacterController>();
        akualnaPredkosc = predkoscPoruszania;
    }

    // Update is called once per frame
    void Update()
    {
        {
            klawiatura();
            myszka();

        }
    }

    
    private void klawiatura()
    {
        rochPrzodTyl = Input.GetAxis("Vertical") * akualnaPredkosc;
        rochLewoPrawo = Input.GetAxis("Horizontal") * akualnaPredkosc;

        
        if (characterControler.isGrounded && Input.GetButton("Jump"))
        {
            aktualnaWysokoscSkoku = wysokoscSkoku;
        }
        else if (!characterControler.isGrounded)
        {
            aktualnaWysokoscSkoku += Physics.gravity.y * Time.deltaTime;
        }

        if (Input.GetKeyDown("left shift"))
        {
            czyBiegnie = true;
        }
        else if (Input.GetKeyUp("left shift"))
        {
            czyBiegnie = false;
        }

        if (czyBiegnie && !czyBrakStaminy())
        {
            akualnaPredkosc = predkoscBiegania;
        }
        else
        {
            akualnaPredkosc = predkoscPoruszania;
        }

        
        Vector3 ruch = new Vector3(rochLewoPrawo, aktualnaWysokoscSkoku, rochPrzodTyl);
        ruch = transform.rotation * ruch;

        characterControler.Move(ruch * Time.deltaTime);

    }

    
    private void myszka()
    {
        
        float myszLewoPrawo = Input.GetAxis("Mouse X") * czuloscMyszki;
        transform.Rotate(0, myszLewoPrawo, 0);

        
        myszGoraDol -= Input.GetAxis("Mouse Y") * czuloscMyszki;

        
        myszGoraDol = Mathf.Clamp(myszGoraDol, -zakresMyszyGoraDol, zakresMyszyGoraDol);
        Camera.main.transform.localRotation = Quaternion.Euler(myszGoraDol, 0, 0);
    }

    
    public bool czyGraczChodzi()
    {
        if (rochPrzodTyl != 0 || rochLewoPrawo != 0)
        {
            return true;
        }
        return false;
    }

    
    public bool czyGraczBiegnie()
    {
        return czyBiegnie;
    }

    private bool czyBrakStaminy()
    {
        if (stamina != null)
        {
            return stamina.brakStaminy();
        }
        return false;
    }

}