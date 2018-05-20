using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour {

    private Light light;
    public CollectorController collectorController;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        light.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.L) && collectorController.isCollected)
            light.enabled = !light.enabled;
	}
}
