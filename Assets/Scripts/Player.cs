using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    float walkingSpeed;
    [SerializeField]
    float rotationSpeed;

    float runningSpeed;

	// Use this for initialization
	void Start () {
        runningSpeed = walkingSpeed * 1.5f;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate () {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        float z;

        if (Input.GetKey("left shift")) {
            z = Input.GetAxis("Vertical") * Time.deltaTime * runningSpeed;
        } else {
            z = Input.GetAxis("Vertical") * Time.deltaTime * walkingSpeed;
        }
            
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}
