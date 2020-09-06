using UnityEngine;
using System.Collections;

public class StartPoint : MonoBehaviour {

    private Transform trans;

    private bool startUstawiony;

	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(!startUstawiony)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                GameObject start = null;
                if (PlayerInstancja.startNr != null && !PlayerInstancja.startNr.Equals(""))
                {
                    start = GameObject.FindGameObjectWithTag(PlayerInstancja.startNr);
                }
                Vector3 pos = trans.position;
                if(start !=null)
                {
                    pos = start.GetComponent<Transform>().position;
                }

                player.GetComponent<Transform>().position = pos;

                startUstawiony = true;
            }
        }
	}
}
