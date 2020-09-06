using UnityEngine;
using System.Collections;

public class DayAndNight : MonoBehaviour {

    private Transform trans;
    public float speed = 5f;

    public Light sun;

    public float maxIntensity = 2f;
    public float minIntensity = 0f;

    public float maxAmbient = 0.7f;
    public float minAmbient = 0f;

    public Color colorDay;
    public Color colorNight;
    private Transform stars;

    public Material day;
    public Material night;

	// Use this for initialization
	void Start ()
    {
        trans = GetComponent<Transform>();
        stars = trans.Find("Stars");
	}
	
	// Update is called once per frame
	void Update ()
    {
        trans.Rotate(speed * Time.deltaTime,0,0);
        setLight();
	}

    private void setLight()
    {
        if(trans.rotation.eulerAngles.x > 0 && trans.rotation.eulerAngles.x <180)
        {
            sun.intensity = maxIntensity;
            RenderSettings.ambientIntensity = maxAmbient;
            RenderSettings.ambientLight = colorDay;

            stars.gameObject.SetActive(false);
            RenderSettings.skybox = day;
        }
        else
        {
            sun.intensity = minIntensity;
            RenderSettings.ambientIntensity = minAmbient;
            RenderSettings.ambientLight = colorNight;
            RenderSettings.fog = false;

            stars.gameObject.SetActive(true);
            RenderSettings.skybox = night;
        }
    }
}
 