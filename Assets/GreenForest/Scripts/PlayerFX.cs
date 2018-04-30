//Original scripting by Aidan Lawrence
using UnityEngine;
using System.Collections;

public class PlayerFX : MonoBehaviour 
{
    public Material skybox;
    public Color fogColor;
    public float transitionDistance = 1000;
    public Color returnColor;
    Light mainLight;
    public float lightMax;
    public float lightMin;
    public float endLevelCoord; // Location where the level has concluded on z axis
	// Use this for initialization
	void Start () 
    {
        mainLight = GameObject.FindGameObjectWithTag("ML").GetComponent<Light>();
        RevertShader();
	}
	
	// Update is called once per frame
	void Update () 
    {
        ControlEnvironment();
	}

    void ControlEnvironment()
    {
        if (gameObject.transform.position.z <= endLevelCoord)
        {
            Vector3 endLevel = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, endLevelCoord);
            float endDistance = Vector3.Distance(gameObject.transform.position, endLevel);
            skybox.SetFloat("_Blend", Mathf.Lerp(1f, 0f, endDistance / transitionDistance));
            skybox.SetColor("_Tint", Color.Lerp(fogColor, returnColor, endDistance / transitionDistance));
            RenderSettings.fogColor = Color.Lerp(Color.black, returnColor, endDistance / transitionDistance);
            mainLight.intensity = Mathf.Lerp(lightMin, lightMax, (endDistance / transitionDistance) / lightMax);
        }
    }

    void OnApplicationQuit()
    {
        RevertShader();
    }

    void RevertShader()
    {
        skybox.SetFloat("_Blend", 0f);
        skybox.SetColor("_Tint", returnColor);
    }
}
