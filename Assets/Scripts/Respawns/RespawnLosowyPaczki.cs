
using UnityEngine;
using System.Collections;

public class RespawnLosowyPaczki : MonoBehaviour
{

    public GameObject respawnObject;
    public float ilosc;


    void Start()
    {
        //lewa czesc wyspy
        for (int i = 0; i < ilosc / 3; i++)
        {
            Vector3 objectPos = new Vector3(0, 0, 0);

            for (int j = 0; j < 10; j++)
            {
                objectPos = new Vector3(Random.Range(363, 159),
                                         Random.Range(105, 110),
                                         Random.Range(57, 103));

                Collider[] hitColliders = Physics.OverlapSphere(objectPos, 1.0f);

                if (hitColliders.Length == 0)
                {
                    break;
                }
            }

            Instantiate(respawnObject, objectPos, new Quaternion(0, 0, 0, 0));

        }

        //gorna czesc wyspy
        for (int i = 0; i < ilosc / 3; i++)
        {
            Vector3 objectPos = new Vector3(0, 0, 0);

            for (int j = 0; j < 10; j++)
            {
                objectPos = new Vector3(Random.Range(53, 118),
                                         Random.Range(105, 110),
                                         Random.Range(155, 259));

                Collider[] hitColliders = Physics.OverlapSphere(objectPos, 1.0f);

                if (hitColliders.Length == 0)
                {
                    break;
                }
            }

            Instantiate(respawnObject, objectPos, new Quaternion(0, 0, 0, 0));
        }

        //prawa czesc wyspy
        for (int i = 0; i < ilosc / 3; i++)
        {
            Vector3 objectPos = new Vector3(0, 0, 0);

            for (int j = 0; j < 10; j++)
            {
                objectPos = new Vector3(Random.Range(164, 318),
                                         Random.Range(110, 115),
                                         Random.Range(339, 317));

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
