using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    int zoom = 20;
    //Field of View Camera komponente
    int normal = 60;
    float smooth = 5;

    private bool isZoomed = false;

    void Update(){

        if (Input.GetMouseButtonDown(1)) {
            isZoomed = !isZoomed;
        }

        if (isZoomed)
        {
            //glatki prijelaz vidnog polja za svaki okvir (eng. frame)
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
        }else {
            //Zoom-out
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
        }
    }

}
