using UnityEngine;
using System.Collections;

public class GroundWeapon : MonoBehaviour
{
    public GUISkin  skin;
    public int weaponNumber;

    private bool isPlayer;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1));
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isPlayer = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.SendMessage("addGun", weaponNumber);
                Destroy(gameObject);
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
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 200, 50), "Naciśnij E aby podnieść");
        }
    }
}