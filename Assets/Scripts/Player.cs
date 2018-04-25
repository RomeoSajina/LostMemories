using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
    float walkingSpeed;
    [SerializeField]
    float rotationSpeed;

    float runningSpeed;

    Animator anim;

    public Image batteryLife;
    public Image fadeImage;

    [SerializeField]
    private float startingTime;
    private float timeLeft;

	// Use this for initialization
	void Start () {
        runningSpeed = walkingSpeed * 1.5f;

        anim = GetComponent<Animator>();

        timeLeft = startingTime;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetAxis("Vertical") != 0) {
            anim.SetBool("isWalking", true);
        } else {
            anim.SetBool("isWalking", false);
        }

        //Crouch
        if(Input.GetKey("c")){
            anim.SetBool("onGround", true);
        }else{
            anim.SetBool("onGround", false);
        }


        //Fade trasition
        if (Input.GetKey("f")) {
            StartCoroutine(FadeIn());
        }

        //Battery Life
        timeLeft -= Time.deltaTime;
        batteryLife.fillAmount = timeLeft / startingTime;
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

    IEnumerator FadeIn () {
        for(float i = 0; i <= 1; i += Time.deltaTime) {
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
