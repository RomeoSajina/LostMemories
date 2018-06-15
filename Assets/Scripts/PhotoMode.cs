using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PhotoMode : MonoBehaviour {

    Texture2D screenCap;
    bool shot = false;
    bool captured = false;
    bool batteryDead = false;

    private GameManager gm;

    public GameObject photoModeUI;
    public Image batteryLife;
    public Image fadeImage;

    public GameObject photoPoint;

    public bool isPhotoModeActive = false;
    private bool isZoomed = false;

    public float startingTime = 180;
    private float timeLeft;

    int zoom = 20;
    //Field of View Camera komponente
    int normal = 60;
    float smooth = 5;


    void Start () {
        screenCap = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        photoModeUI.SetActive(false);
        timeLeft = startingTime;
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
	
	void Update () {
        HandlePhotoMode();
        Debug.DrawLine(transform.position, transform.forward, Color.blue);
    }

    private void OnGUI() {
        if (shot){
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), screenCap, ScaleMode.StretchToFill);
            
        }
    }

    void HandlePhotoMode () {
        if (Input.GetKeyDown("f")) {
            StartCoroutine(FadeIn());
            gm.ToggleMovement();
            AudioManager.instance.StopAll();


            if (isPhotoModeActive){
                isZoomed = false;
            }
            isPhotoModeActive = !isPhotoModeActive;
        }

        if (Input.GetMouseButtonDown(0) && isPhotoModeActive && !captured) {
            HandleDetection();
   
            StartCoroutine(Capture());
            AudioManager.instance.Play("photo");
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

        if (batteryLife.fillAmount <= 0 && !batteryDead) {
            StartCoroutine(BatteryDead());
            batteryDead = true;
        }
    }

    void HandleDetection () {   
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            Debug.Log(hit.transform.tag);
            float camX, camZ;
            camX = transform.position.x;
            camZ = transform.position.z; 

            if(photoPoint == null) {
                if (hit.transform.tag == "Enemy"){
                    Debug.Log("Uslikano!");
                    captured = true;
                    //gm.HandleWin();
                }
            } else {
                if (Mathf.Abs(photoPoint.transform.position.x - camX) < 5 && Mathf.Abs(photoPoint.transform.position.z - camZ) < 5 && hit.transform.tag == "Enemy") {
                    Debug.Log("Uslikano!");
                    captured = true;
                    //gm.HandleWin();
                }
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

        if (captured) {
            byte[] bytes = screenCap.EncodeToPNG();
            GameManager.instance.SaveImage(bytes);
            //File.WriteAllBytes(Application.dataPath + "/SavedScreen.png", bytes);
        }

        shot = true;
        Time.timeScale = .0000001f;
        yield return new WaitForSeconds(2*Time.timeScale);
        shot = false;
        Time.timeScale = 1;

        if(captured)
            gm.HandleWin();
    }


    IEnumerator BatteryDead() {
        AudioManager.instance.Play("low_battery");

        yield return new WaitForSeconds(2);
        AudioManager.instance.Play("low_battery");

        yield return new WaitForSeconds(10);
        AudioManager.instance.PlayNarrator("battery");
    }

}
