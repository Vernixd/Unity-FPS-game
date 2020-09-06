using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnLosowy2 : MonoBehaviour {

    public GameObject respawnObject;
    public Text enemyText;
    public float ilosc;
    static public float iloscPrzekazanie;




    void Start()
    {
        enemyText.text = "Ilosc przeciwnikow: " + EnemyAI2.iloscEnemy;
        iloscPrzekazanie = ilosc;
        
        for (int i = 0; i < ilosc; i++)
        {
            Vector3 objectPos = new Vector3(0, 0, 0);

            for (int j = 0; j < 10; j++)
            {
                objectPos = new Vector3(Random.Range(1, 1001),
                                         1000,
                                         Random.Range(1, 1001));

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
        enemyText.text = "Ilosc przeciwnikow: " + EnemyAI2.iloscEnemy;
    }

    public void setIlosc(float iloscW)
    {
        ilosc = iloscW;
    }
}
