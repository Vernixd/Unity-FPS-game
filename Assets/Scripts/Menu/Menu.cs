using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public float buttonWidth = 300;
    public float buttonHeight = 60;
    public Texture GameLogo;
    public GUISkin skin;
    

    private float buttonMargin = 10;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update () {

    }

    void OnGUI ()
    {
        GUI.DrawTexture(new Rect(0,0, 700, 200), GameLogo);
        GUI.skin = skin;

        GUI.BeginGroup(new Rect(300, 300, buttonWidth, (buttonHeight + buttonMargin) * 3));

        if (GUI.Button(new Rect(0, 0, buttonWidth, buttonHeight), "Nowa gra"))
        {
            SceneManager.LoadScene("Game");
            Destroy(GameObject.Find("mjuzik"));
        }
        if (GUI.Button(new Rect(0, buttonHeight + buttonMargin, buttonWidth, buttonHeight), "Opcje"))
        {
            SceneManager.LoadScene("Opcje");
        }
        if (GUI.Button(new Rect(0, (buttonHeight + buttonMargin) * 2, buttonWidth, buttonHeight), "Wyjście"))
        {
            Application.Quit();
        }
        GUI.EndGroup();
    }

}
