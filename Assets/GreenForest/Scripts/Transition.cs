//Original scripting by Aidan Lawrence

using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour
{

    // Use this for initialization
    public Color fadeInColor;
    public float fadeSpeed = 0f;
    public bool fadeOut = false;
    GUITexture gt;
    void Start()
    {
        gt = gameObject.GetComponent<GUITexture>();
        gt.color = fadeInColor;
    }

    // Update is called once per frame
    float reloadTimer = 5.0f;
    void Update()
    {
        if (!fadeOut)
        {
            gt.color = Color.Lerp(gt.color, new Color(gt.color.r, gt.color.g, gt.color.b, 0f), Time.deltaTime * fadeSpeed);
        }
            
        if (fadeOut)
        {
            gt.color = Color.Lerp(gt.color, new Color(gt.color.r, gt.color.g, gt.color.b, 1f), Time.deltaTime * fadeSpeed/4.0f);
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0f)
                Application.LoadLevel(Application.loadedLevel);
        }
            
    }
}
