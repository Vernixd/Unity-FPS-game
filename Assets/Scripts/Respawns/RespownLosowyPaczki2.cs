using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespownLosowyPaczki2 : MonoBehaviour {

    public GameObject respawnObject;
    public float ilosc;


    void Start()
    {
        //lewa czesc wyspy
        for (int i = 0; i < ilosc; i++)
        {
            Vector3 objectPos = new Vector3(0, 0, 0);

            for (int j = 0; j < 10; j++)
            {
                objectPos = new Vector3(Random.Range(100, 900),
                                         1000,
                                         Random.Range(100, 900));

                Collider[] hitColliders = Physics.OverlapSphere(objectPos, 1.0f);

                if (hitColliders.Length == 0)
                {
                    break;
                }
            }

            Instantiate(respawnObject, objectPos, new Quaternion(0, 0, 0, 0));

        }

        
    }
}
