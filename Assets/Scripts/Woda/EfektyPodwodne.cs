using UnityEngine;
using System.Collections;

public class EfektyPodwodne : MonoBehaviour {

    //Nowe ustawienia mgły..
    public bool fog;
    public Color kolorMgly;
    public float gestoscMgly;
    public FogMode trybMgly;
    public float startMgly;
    public float koniecMgly;

    //Pierwotne ustawienia efektów.
    private bool tmpFog; 
    private Color tmpKolorMgly;
    private float oldGestoscMgly;
    private FogMode oldTrybMgly;
    private float tmpStartMgly;
    private float tmpKoniecMgly; 

    void Start()
    {
    }

    
    //Metoda wywoływana zanim kamera zacznie wyświetlać obraz.
   
    void OnPreRender()
    {
        tmpFog = RenderSettings.fog;
        tmpKolorMgly = RenderSettings.fogColor;
        oldGestoscMgly = RenderSettings.fogDensity;
        oldTrybMgly = RenderSettings.fogMode;
        tmpStartMgly = RenderSettings.fogStartDistance;
        tmpKoniecMgly = RenderSettings.fogEndDistance;
      
        RenderSettings.fog = fog;
        RenderSettings.fogColor = kolorMgly;
        RenderSettings.fogDensity = gestoscMgly;
        RenderSettings.fogMode = trybMgly;
        RenderSettings.fogStartDistance = startMgly;
        RenderSettings.fogEndDistance = koniecMgly;
    }

    
    //Metoda wywoływana po zakończeniu wyświetlania obrazu przez kamerę.
    void OnPostRender()
    {    
        RenderSettings.fog = tmpFog;
        RenderSettings.fogColor = tmpKolorMgly;
        RenderSettings.fogDensity = oldGestoscMgly;
        RenderSettings.fogMode = oldTrybMgly;
        RenderSettings.fogStartDistance = tmpStartMgly;
        RenderSettings.fogEndDistance = tmpKoniecMgly;
    }

}