using UnityEngine;
using System.Collections;

public class PlayerInstancja : MonoBehaviour {

    public static PlayerInstancja instancja;
    public static string startNr;

    public static bool respown = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
