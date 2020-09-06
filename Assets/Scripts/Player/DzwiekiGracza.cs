using UnityEngine;
using System.Collections;

public class DzwiekiGracza : MonoBehaviour {
    private Transform trans;

    public CharacterController characterControler;
    private StaminaUI stamina;

    public AudioSource zrodloDzwieku;

    public AudioClip dzwiekChodzenie;

    public AudioClip dzwiekSkoku;
    public AudioClip dzwiekLadowania;
    public float odliczanieDoKroku = 0f;
    public float czasKroku = 0.6f;

    public bool graczNaZiemi;

    private PlayerControler playerControler;
    private GraczWodaKontroler graczWodaKontroler;

    public DzwiekDlaTekstury[] dzwieki;
    private DzwiekDlaTekstury aktualnyDzwiek;
    private bool kolizjaZObiektem = false;

    // Use this for initialization
    void Start()
    {
        trans = GetComponent<Transform>();
        playerControler = GetComponent<PlayerControler>();
        graczWodaKontroler = GetComponent<GraczWodaKontroler>();
        stamina = GetComponent<StaminaUI>();
        ustawDomyslnyDzwiekKroku();
    }

    // Update is called once per frame
    void Update()
    {
        if (zrodloDzwieku != null)
        {
            pobierzDzwiek();
            dzwiekChodzenia();
        }
    }

    
    private void dzwiekChodzenia()
    {
        if (odliczanieDoKroku > 0)
        {
            if (czyGraczBiegnie() && !czyBrakStaminy())
            {
                odliczanieDoKroku -= Time.deltaTime * 1.3f;
            }
            else
            {
                odliczanieDoKroku -= Time.deltaTime;
            }
        }

        if (czyGraczChodzi() && characterControler.isGrounded && odliczanieDoKroku <= 0)
        {
            odliczanieDoKroku = czasKroku;
            zrodloDzwieku.PlayOneShot(aktualnyDzwiek.dzwiek);
        }
        if (Input.GetButton("Jump") && characterControler.isGrounded)
        {
            zrodloDzwieku.PlayOneShot(dzwiekSkoku);
        }
        
        if (!graczNaZiemi && characterControler.isGrounded)
        {
            zrodloDzwieku.PlayOneShot(dzwiekLadowania);
        }
        graczNaZiemi = characterControler.isGrounded;
    }

    private bool czyBrakStaminy()
    {
        if (stamina != null)
        {
            return stamina.brakStaminy();
        }
        return false;
    }

    
    private void pobierzDzwiek()
    {
        if (!kolizjaZObiektem)
        {
            bool pobrany = false;
            foreach (DzwiekDlaTekstury dzwiek in dzwieki)
            {
                if (dzwiek.tekstura != null && dzwiek.tekstura.name.Equals(PowierzchniaTerenu.NazwaTeksturyWPozycji(trans.position)))
                {
                    aktualnyDzwiek = dzwiek;
                    pobrany = true;
                    break;
                }
            }
            if (!pobrany)
            {
                aktualnyDzwiek = dzwieki[0];
            }
        }
    }

    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Transform trans = hit.collider.gameObject.GetComponent<Transform>();

        if (trans.tag == "Teren")
        { 
            kolizjaZObiektem = false; 
        }
        else
        {
            DzwiekiDlaObiektu ddo = trans.GetComponent<DzwiekiDlaObiektu>();
            if (ddo != null && ddo.dzwiekDlaTekstury.dzwiek != null)
            {
                aktualnyDzwiek = ddo.dzwiekDlaTekstury; 
            }
            else
            {
                
                aktualnyDzwiek = dzwieki[0];
            }
            kolizjaZObiektem = true; 
        }
    }

    private void ustawDomyslnyDzwiekKroku()
    {
        if (dzwieki.Length > 0 && dzwieki[0].dzwiek != null)
        {
            aktualnyDzwiek = dzwieki[0];
        }
    }

    private bool czyGraczChodzi()
    {
        if (playerControler.enabled && playerControler.czyGraczChodzi())
        {
            return true;
        }
        if (graczWodaKontroler.enabled && graczWodaKontroler.czyGraczChodzi())
        {
            return true;
        }
        return false;
    }

    private bool czyGraczBiegnie()
    {
        if (playerControler.enabled && playerControler.czyGraczBiegnie())
        {
            return true;
        }
        if (graczWodaKontroler.enabled && graczWodaKontroler.czyGraczBiegnie())
        {
            return true;
        }
        return false;
    }

}