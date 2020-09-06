using UnityEngine;
using System.Collections;

public class Ammopack : MonoBehaviour
{
    public GUISkin skin;
    public float ammunition = 25.0f;
    public float gunType;
    private bool isPlayer;

    // Use this for initialization
    void Start () {
        gunType = Random.Range(1, 4);
	}

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0));
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag.Equals("Player"))
    //    {
            
    //        GunsInventory inventory = other.GetComponent<GunsInventory>();
    //        inventory.SendMessage("addAmmo", new Vector2(ammunition, gunType));
    //        Destroy(gameObject);
    //    }
    //}

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isPlayer = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.SendMessage("addAmmo", new Vector2(ammunition, gunType));
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
            if (gunType == 1)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 10, 200, 100), "Podnies amunicje do pistoletu");
            }
            else if (gunType == 2)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 10, 200, 100), "Podnies amunicje do karabinku");
            }
            else if (gunType == 3)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 10, 200, 100), "Podnies amunicje do strzelby");
            }
        }
    }
}
