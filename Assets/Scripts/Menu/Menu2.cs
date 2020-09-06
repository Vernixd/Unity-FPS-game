using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu2 : MonoBehaviour
{
    public float buttonWidth = 300;
    public float buttonHeight = 90;
    public Texture GameLogo;
    public GUISkin skin;


    private float buttonMargin =10;

    // Use this for initialization
    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Cursor.visible = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 700, 200), GameLogo);
        GUI.skin = skin;

        GUI.BeginGroup(new Rect(300,300, buttonWidth, (buttonHeight + buttonMargin) * 3));

        if (GUI.Button(new Rect(0, 0, buttonWidth, buttonHeight), "Zagraj jeszcze raz"))
        {
            SceneManager.LoadScene("Game");
        }
        if (GUI.Button(new Rect(0, (buttonHeight + buttonMargin) * 2, buttonWidth, buttonHeight), "Zakończ gre"))
        {
            Application.Quit();
        }
        GUI.EndGroup();
    }

}
