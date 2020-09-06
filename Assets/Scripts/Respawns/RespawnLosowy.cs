using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RespawnLosowy : MonoBehaviour {

    public GameObject respawnObject;
    public Text enemyText;
    public float ilosc;
    static public float iloscPrzekazanie;
    

    

    void Start()
    {
        enemyText.text = "Ilosc przeciwnikow: " + EnemyAI.iloscEnemy;
        iloscPrzekazanie = ilosc;
        //lewa czesc wyspy
        for (int i = 0; i < ilosc / 3; i++)
        {
            Vector3 objectPos = new Vector3(0, 0, 0);

            for (int j = 0; j < 10; j++)
            {
                objectPos = new Vector3(Random.Range(363, 159),
                                         Random.Range(105, 110),
                                         Random.Range(57,103) );

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
                objectPos = new Vector3(Random.Range(164, 290),
                                         Random.Range(110, 115),
                                         Random.Range(320, 340));

                Collider[] hitColliders = Physics.OverlapSphere(objectPos, 1.0f);

                if (hitColliders.Length == 0)
                {
                    break;
                }
            }

            Instantiate(respawnObject, objectPos, new Quaternion(0, 0, 0, 0));
        }
    }

     void Update()
    {
        enemyText.text = "Ilosc przeciwnikow: " + EnemyAI.iloscEnemy;
    }

    public void setIlosc(float iloscW)
    {
        ilosc = iloscW;
    }
}
