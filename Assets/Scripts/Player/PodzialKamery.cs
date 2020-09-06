using UnityEngine;
using System.Collections;

public class PodzialKamery : MonoBehaviour {

    private Transform trans;

    private Camera kameraGlowna;
    private Camera kameraWWodzie;
    private Transform kamWWodzieTrans;
    private GameObject woda;

    private EfektyPodwodne efektyPodwodne;

    public float poziomPodzialuEkranu;

    void Start()
    {
        trans = GetComponent<Transform>();
        kameraGlowna = GetComponent<Camera>();

        kamWWodzieTrans = trans.Find("KameraWWodzie");
        kameraWWodzie = kamWWodzieTrans.GetComponent<Camera>();

        kamWWodzieTrans.gameObject.SetActive(false);
        efektyPodwodne = kamWWodzieTrans.GetComponent<EfektyPodwodne>();
    }

    void Update()
    {
        ustawPodzial();
        wlaczEfektWody();

        if (getWodaTrans() != null && czyUstawicPodzialuEkranu())
        {

            if (poziomPodzialuEkranu > 0 && poziomPodzialuEkranu < 1)
            {
                ustawPodzialEkranu();
            }
        }
        else
        { 
            resetKameryGlownej();
            poziomPodzialuEkranu = 0;
        }
    }

    
    void ustawPodzialEkranu()
    {
        Camera zanurzonaKamera = kameraWWodzie;

        float polowaWysokosci = Mathf.Tan(zanurzonaKamera.fieldOfView * Mathf.Deg2Rad * .5f) * zanurzonaKamera.nearClipPlane;
        float gornaCzescGory = polowaWysokosci;
        float dolnaczescGory = (poziomPodzialuEkranu - .5f) * polowaWysokosci * 2f;
        float gornaCzescDolu = dolnaczescGory;
        float dolnaCzescDolu = -polowaWysokosci;

        Matrix4x4 macierzKameryGlownej = kameraGlowna.projectionMatrix;
        macierzKameryGlownej[1, 1] = (2f * zanurzonaKamera.nearClipPlane) / (gornaCzescGory - dolnaczescGory);
        macierzKameryGlownej[1, 2] = (gornaCzescGory + dolnaczescGory) / (gornaCzescGory - dolnaczescGory);

        Matrix4x4 macierzKameryDolnej = zanurzonaKamera.projectionMatrix;
        macierzKameryDolnej[1, 1] = (2f * zanurzonaKamera.nearClipPlane) / (gornaCzescDolu - dolnaCzescDolu);
        macierzKameryDolnej[1, 2] = (gornaCzescDolu + dolnaCzescDolu) / (gornaCzescDolu - dolnaCzescDolu);

        kameraGlowna.projectionMatrix = macierzKameryGlownej;
        zanurzonaKamera.projectionMatrix = macierzKameryDolnej;

        Rect rectDlaKamDolnej = zanurzonaKamera.rect;
        rectDlaKamDolnej.height = poziomPodzialuEkranu;
        zanurzonaKamera.rect = rectDlaKamDolnej;

        Rect rectDlaKamGornej = kameraGlowna.rect;
        rectDlaKamGornej.height = 1f - poziomPodzialuEkranu;
        rectDlaKamGornej.y = poziomPodzialuEkranu;
        kameraGlowna.rect = rectDlaKamGornej;
    }

    
    private void ustawPodzial()
    {

        if (getWodaTrans() != null)
        { 

            if (czyUstawicPodzialuEkranu())
            { 

                
                float rozmiarEkranu = Vector3.Distance(getDolnaCzescKamery(), getGornaCzescKamery());

                
                Vector3 kierunekPromienia = getDolnaCzescKamery() - getGornaCzescKamery();

                
                Ray ray = new Ray(getGornaCzescKamery(), kierunekPromienia);

                
                RaycastHit hitInfo;

                
                int warstwaDoIgnorowania = ~(1 << 9);

                
                if (Physics.Raycast(ray, out hitInfo, rozmiarEkranu, warstwaDoIgnorowania))
                {

                    
                    Vector3 hitPoint = hitInfo.point;
                    Debug.DrawRay(getGornaCzescKamery(), hitPoint - getGornaCzescKamery(), Color.red);
                    
                    float zanurzenie = Vector3.Distance(getDolnaCzescKamery(), hitPoint);

                    
                    poziomPodzialuEkranu = zanurzenie / rozmiarEkranu;
                }

            }
            else if (getDolnaCzescKamery().y > getWodaTrans().position.y)
            { 
                resetKameryGlownej();
                resetKameryWody();
                poziomPodzialuEkranu = 0;
            }
            else if (getGornaCzescKamery().y < getWodaTrans().position.y)
            { 
                resetKameryGlownej();
                resetKameryWody();
                poziomPodzialuEkranu = 1;
            }
        }
    }

   
    private bool czyUstawicPodzialuEkranu()
    {
        return getDolnaCzescKamery().y < getWodaTrans().position.y && getGornaCzescKamery().y > getWodaTrans().position.y;
    }

    
    private Vector3 getDolnaCzescKamery()
    {
        Vector3 p = kameraGlowna.ScreenToWorldPoint(new Vector3(0, 0, kameraGlowna.nearClipPlane));
        return p;
    }

    
    private Vector3 getGornaCzescKamery()
    {
        Vector3 p = kameraGlowna.ScreenToWorldPoint(new Vector3(0, Screen.height - 1, kameraGlowna.nearClipPlane));
        return p;
    }

    
    private void resetKameryGlownej()
    {
        Rect rect = new Rect(0f, 0f, 1f, 1f);
        Camera.main.rect = rect;
        kameraGlowna.ResetProjectionMatrix();
    }

   
    private void resetKameryWody()
    {
        Rect rect = new Rect(0f, 0f, 1f, 1f);
        kameraWWodzie.rect = rect;
        kameraWWodzie.ResetProjectionMatrix();
    }

    private void wlaczEfektWody()
    {
        if (getWodaTrans() != null)
        {
            if (getDolnaCzescKamery().y < getWodaTrans().position.y)
            {
                efektyPodwodne.fog = true;
            }
            else
            {
                efektyPodwodne.fog = false;
            }
        }
    }



    
    private Transform getWodaTrans()
    {
        if (woda != null)
        {
            return woda.GetComponent<Transform>();
        }
        return null;
    }

    
    public void setWoda(GameObject woda)
    {
        this.woda = woda;
    }
}

