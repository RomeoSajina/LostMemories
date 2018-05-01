using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoMode : MonoBehaviour {

    private GameManager gm;

    public GameObject photoModeUI;
    public Image batteryLife;
    public Image fadeImage;

    public bool isPhotoModeActive = false;

    public float startingTime = 60;
    private float timeLeft;


    void Start () {
        photoModeUI.SetActive(false);
        timeLeft = startingTime;
        gm = GameManager.instance;
    }
	
	void Update () {
        HandlePhotoMode();
	}

    void HandlePhotoMode () {
        if (Input.GetKeyDown("f")) {
            StartCoroutine(FadeIn());
            gm.ToggleMovement();
            isPhotoModeActive = !isPhotoModeActive;
        }

        /*Photo mode UI se samo prikazuje kad je aplha slike veca od 90%, tj. kada je korisniku ekran dovoljno
        zamracen da ne primjeti pojavljivanje UI-a*/
        if (fadeImage.color.a >= 0.9) {
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
