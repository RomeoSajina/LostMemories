﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Animator anim;
    private Rigidbody rb;

    private Transform startingPosition;

    public GameManager gm;

    public float walkingSpeed = 3f;
    public float lookSensitivity = 3f;

    public Camera camera;

    private void Awake () {
        startingPosition = transform;   
    }

    private void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        anim.SetBool("isStanding", true);
    }

    private void Update () {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            anim.SetBool("isStanding", false);
        } else {
            anim.SetBool("isStanding", true);
        }
    }

    private void FixedUpdate () {
        if (!gm.canMove) {
            HandleMovement();
            HandleRotation();
        }
    }

    public void MoveToStaringPosition () {
        this.transform.position = startingPosition.position;

        gm.canMove = true;
    }

    void HandleMovement () {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * -xMov;
        Vector3 movVertical = transform.forward * -zMov;

        Vector3 velocity;

        velocity = (movHorizontal + movVertical).normalized * walkingSpeed;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    void HandleRotation () {
        //Rotation
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, -yRot, 0f) * lookSensitivity;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        //Camera rotation
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(-xRot, 0f, 0f) * lookSensitivity;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        camera.transform.Rotate(-cameraRotation);
    }
}
