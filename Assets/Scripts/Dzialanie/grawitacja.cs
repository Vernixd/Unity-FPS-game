using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grawitacja : MonoBehaviour {

    public Vector3 Grawitacja;
    public CharacterController kontroler;

	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update ()
    {
        kontroler = GetComponent<CharacterController>();
        if(kontroler.isGrounded)
        {

        }
        else
        {
            Grawitacja = new Vector3(0, -1000, 0);
            kontroler.Move(Grawitacja);
        }


    }
}
