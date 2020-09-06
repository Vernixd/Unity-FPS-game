using UnityEngine;
using System.Collections;

public class InterfaceInstacja : MonoBehaviour {

    public static InterfaceInstacja instancja;
    
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