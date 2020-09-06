using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class Shooting : MonoBehaviour
{
    public GameObject bulletHole;
    public Texture2D crosshairTexture;
    public AudioClip pistolShot;
    public AudioClip reloadSound;
    public int maxAmmo = 200;
    public int clipSize = 10;
    public Text ammoText;
    public Text reloadText;
    public float reloadTime = 2.0f;

    public GameObject bloodParticles;
    public int damage = 5;

    public bool automatic = false;
    public float shotDelay = 0.5f;
    private float shotDelayCounter = 0.0f;

    public int currentAmmo = 30;
    public int currentClip;
    private Rect position;
    private float range = 150.0f;
    private GameObject pistolSparks;
    private Vector3 fwd;
    private RaycastHit hit;
    private bool isReloading = false;

    private float timer = 0.0f;

    private float zoomFieldOfView = 40.0f;
    private float defaultFieldOfView = 60.0f;

    // Use this for initialization
    void Start()
{
    currentClip = clipSize;
    position = new Rect((Screen.width - crosshairTexture.width) / 2 ,(Screen.height - crosshairTexture.height) / 2, crosshairTexture.width,crosshairTexture.height);

        pistolSparks = GameObject.Find("Sparks");
	    pistolSparks.GetComponent<ParticleSystem>().enableEmission = false;
        GetComponent<AudioSource>().clip = pistolShot;
        ammoText.text = "Amunicja : " + currentClip + " / " + currentAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = "Amunicja : " + currentClip + " / " + currentAmmo;
        if (currentClip == 0)
        {
            reloadText.enabled = true;
        }
        else
        {
            reloadText.enabled = false;
        }

        if (gameObject.GetComponentInParent<Camera>() is Camera)
        {
            Camera cam = gameObject.GetComponentInParent<Camera>();
            if (Input.GetButton("Fire2"))
            {
                if (cam.fieldOfView > zoomFieldOfView)
                {
                    cam.fieldOfView--;
                }
            }
            else
            {
                if (cam.fieldOfView < defaultFieldOfView)
                {
                    cam.fieldOfView++;
                }
            }
        }

        if (shotDelayCounter > 0)
        {
            shotDelayCounter -= Time.deltaTime;
        }

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
       

        if (Input.GetButtonDown("Fire1") && currentClip > 0 && !isReloading || Input.GetButton("Fire1") && automatic && currentClip > 0 && !isReloading && shotDelayCounter <= 0)
        {
            shotDelayCounter = shotDelay;
            currentClip--;

            GetComponent<AudioSource>().Play();
            pistolSparks.GetComponent<ParticleSystem>().enableEmission = true;
            Debug.DrawRay(transform.position,fwd, Color.green);
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPoint = hit.point;
                if (hit.transform.tag == "Enemy" && hit.distance < range)
                {
                    Debug.Log("Trafiony przeciwnik");
                    hit.transform.gameObject.SendMessage("takeHit", damage);
                    GameObject go;
                    go = Instantiate(bloodParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
                    Destroy(go, 0.3f);

                }
                else if (hit.distance < range)
                {
                    GameObject go;
                    go = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
                    Destroy(go, 5);
                    Debug.Log("Trafiona Sciana");
                }
            }
        }
        else
        {
            pistolSparks.GetComponent<ParticleSystem>().enableEmission = false;
        }

        if (((Input.GetButtonDown("Fire1") && currentClip == 0) || Input.GetButtonDown("Reload")) && currentClip < clipSize)
        {
            if (currentAmmo > 0)
            {
                GetComponent<AudioSource>().clip = reloadSound;
                GetComponent<AudioSource>().Play();
                isReloading = true;
            }
        }

        if (isReloading)
        {
            timer += Time.deltaTime;
            if (timer >= reloadTime)
            {
                int needAmmo = clipSize - currentClip;

                if (currentAmmo >= needAmmo)
                {
                    currentClip = clipSize;
                    currentAmmo -= needAmmo;
                }
                else
                {
                    currentClip += currentAmmo;
                    currentAmmo = 0;
                }

                GetComponent<AudioSource>().clip = pistolShot;
                isReloading = false;
                timer = 0.0f;
            }
        }

    }

       void OnGUI()
    {
        GUI.DrawTexture(position, crosshairTexture);
    }

    public bool canGetAmmo()
    {
        if (currentAmmo == maxAmmo)
        {
            return false;
        }
        return true;
    }

    void addAmmo(Vector2 data)
    {
        int ammoToAdd = (int)data.x;

        if (maxAmmo - currentAmmo >= ammoToAdd)
        {
            currentAmmo += ammoToAdd;
        }
        else
        {
            currentAmmo = maxAmmo;
        }
    }

    public void setAmmo(int clip, int ammo)
    {
        currentClip = clip;
        currentAmmo = ammo;
    }


}
