using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private GameManager gm;
    private Animator anim;
    private Rigidbody rb;

    public CapsuleCollider colliderStanding;

    public float speed = 5f;
    public float lookSensitivity = 3f;

    public Camera camera;

	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        gm = GameManager.instance;
    }
	
	void Update () {
        if (gm.canMove) {
            HandleAnimatorStates();
        }     
    }

    private void FixedUpdate () {
        if (gm.canMove) {
            HandleMovement();
        }
        HandleRotation();
    }

    void HandleMovement () {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    void HandleRotation () {
        //Rotation
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        //Camera rotation
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        camera.transform.Rotate(-cameraRotation);
    }

    void HandleAnimatorStates () {
        //Walking animaton
        if (Input.GetAxis("Vertical") != 0) {
            anim.SetBool("isWalking", true);
        } else {
            anim.SetBool("isWalking", false);
        }

        //Walking backwards animaton
        if (Input.GetAxis("Vertical") < 0){
            anim.SetFloat("Direction", -1.0f);
        }else{
            anim.SetFloat("Direction", 1.0f);
        }

        //Running animation
        if (Input.GetKey("left shift")) {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        //Crouch Animation
        if (Input.GetKey("c") || Input.GetKey("left ctrl")) {
            anim.SetBool("isCrouching", true);
            Vector3 vector = new Vector3(0, 90, 15);
            camera.transform.localPosition = vector;

            Vector3 centerC = new Vector3(.6f, 42, -2);

            colliderStanding.center = centerC;
            colliderStanding.height = 85;
        } else {
            anim.SetBool("isCrouching", false);
            Vector3 vector = new Vector3(0, 150, 15);
            camera.transform.localPosition = vector;

            Vector3 centerS = new Vector3(.6f, 82, -2);

            colliderStanding.center = centerS;
            colliderStanding.height = 164;
        }
    }
}
