using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PhotoMode : MonoBehaviour {

    Texture2D screenCap;
    bool shot = false;

    private GameManager gm;

    public GameObject photoModeUI;
    public Image batteryLife;
    public Image fadeImage;

    public bool isPhotoModeActive = false;
    private bool isZoomed = false;

    public float startingTime = 60;
    private float timeLeft;

    int zoom = 20;
    //Field of View Camera komponente
    int normal = 60;
    float smooth = 5;


    void Start () {
        screenCap = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        photoModeUI.SetActive(false);
        timeLeft = startingTime;
        gm = GameManager.instance;
    }
	
	void Update () {
        HandlePhotoMode();
        Debug.DrawLine(transform.position, transform.forward, Color.blue);
    }

    private void OnGUI()
    {
        if (shot){
            GUI.DrawTexture(new Rect(10, 10, 60, 40), screenCap, ScaleMode.StretchToFill);
        }
    }

    void HandlePhotoMode () {
        if (Input.GetKeyDown("f")) {
            StartCoroutine(FadeIn());
            gm.ToggleMovement();
            isPhotoModeActive = !isPhotoModeActive;
        }

        if (Input.GetMouseButtonDown(0) && isPhotoModeActive){
            StartCoroutine(Capture());
            HandleDetection();
        }

        if (Input.GetMouseButtonDown(1) && isPhotoModeActive){
            isZoomed = !isZoomed;
        }

        if (isZoomed){
            //glatki prijelaz vidnog polja za svaki okvir (eng. frame)
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
        }
        else{
            //Zoom-out
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
        }

        /*Photo mode UI se samo prikazuje kad je aplha slike veca od 90%, tj. kada je korisniku ekran dovoljno
        zamracen da ne primjeti pojavljivanje UI-a*/
        if (fadeImage.color.a >= 0.9) {
            photoModeUI.SetActive(isPhotoModeActive);
        }

        timeLeft -= Time.deltaTime;
        batteryLife.fillAmount = timeLeft / startingTime;
    }

    void HandleDetection () {
        Debug.DrawLine(transform.position, transform.forward, Color.blue);
        //Debug.Log(raycastTarget.position);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Player") {
                Debug.Log("Player");
                Time.timeScale = 0;
            }
        }
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

    IEnumerator Capture() {
        yield return new WaitForEndOfFrame();
        screenCap.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
        screenCap.Apply();

        byte[] bytes = screenCap.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/SavedScreen.png", bytes);

        shot = true;
    }
}
