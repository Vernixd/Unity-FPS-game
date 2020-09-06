using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Menu");
    }
}
