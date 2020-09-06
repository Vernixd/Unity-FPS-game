using UnityEngine;
using System.Collections;

public class Gun3Instancja : MonoBehaviour {

    public static Gun3Instancja instancja;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        if (!instancja)
        {
            DontDestroyOnLoad(this.gameObject);
            instancja = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}