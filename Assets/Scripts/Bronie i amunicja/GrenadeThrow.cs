using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrenadeThrow : MonoBehaviour
{
    public Rigidbody grenade;
    
    public Text grenadeText;
    private int currentGrenades = 2;

    // Use this for initialization
    void Start ()
    {
	    grenadeText.text = "Granaty : " + currentGrenades;
    }
	

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentGrenades > 0)
        {
            Rigidbody clone = Instantiate(grenade, transform.position, transform.rotation) as Rigidbody;
            clone.AddForce(transform.TransformDirection(Vector3.forward * 1000));
            currentGrenades--;
            grenadeText.text = "Granaty : " + currentGrenades;
        }
    }
}
