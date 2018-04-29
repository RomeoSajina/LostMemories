using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public bool canMove;
    public float rotationSpeed;

    Animator anim;

    public GameObject cameraZoom;
    public GameObject photoModeUI;
    public Image batteryLife;
    public Image fadeImage;

    public bool isPhotoModeActive = false;

    public float startingTime;
    private float timeLeft;


	void Start () {
        anim = GetComponent<Animator>();
        cameraZoom.GetComponent<CameraZoom>().enabled = false;

        canMove = true;
        photoModeUI.SetActive(false);
        timeLeft = startingTime;
	}
	
	void Update () {
        if(canMove) {
            HandleAnimatorStates();
        }
        HandlePhotoMode();
    }

    private void FixedUpdate () {
        HandleRotation();
    }

    void HandleRotation () {
        float rotationY = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        transform.Rotate(0, rotationY, 0);
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
        } else {
            anim.SetBool("isCrouching", false);
        }
    }

    void HandlePhotoMode () {
        if (Input.GetKeyDown("f")) {
            StartCoroutine(FadeIn());
            canMove = !canMove;
            isPhotoModeActive = !isPhotoModeActive;
            cameraZoom.GetComponent<CameraZoom>().enabled = !cameraZoom.GetComponent<CameraZoom>().enabled;
        }

        /*Photo mode UI se samo prikazuje kad je aplha slike veca od 90%, tj. kada je korisniku ekran dovoljno
        zamracen da ne primjeti pojavljivanje UI-a*/
        if(fadeImage.color.a >= 0.9) {
            photoModeUI.SetActive(isPhotoModeActive);
        }
        
        timeLeft -= Time.deltaTime;
        batteryLife.fillAmount = timeLeft / startingTime;
    }


    //Photo mode transition
    IEnumerator FadeIn () {
        for (float i = 0; i <= 1; i += Time.deltaTime) {
            fadeImage.color = new Color(0, 0, 0, i);
 
            yield return null;

            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut () {
        for (float i = 1; i >= 0; i -= Time.deltaTime) {
            fadeImage.color = new Color(0, 0, 0, i);

            yield return null;
        }
    }

}
