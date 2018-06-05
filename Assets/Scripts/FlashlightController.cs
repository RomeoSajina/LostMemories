using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour {

    private Light flashLight;
    public CollectorController collectorController;

	// Use this for initialization
	void Start () {
        flashLight = GetComponent<Light>();
        flashLight.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.L) && collectorController.isCollected)
            flashLight.enabled = !flashLight.enabled;
	}
}
