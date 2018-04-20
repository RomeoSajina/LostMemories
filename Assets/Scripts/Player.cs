using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    float walkingSpeed;
    [SerializeField]
    float rotationSpeed;

    float runningSpeed;

    Animator anim;

	// Use this for initialization
	void Start () {
        runningSpeed = walkingSpeed * 1.5f;

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetAxis("Vertical") != 0) {
            anim.SetBool("isWalking", true);
        } else {
            anim.SetBool("isWalking", false);
        }
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
