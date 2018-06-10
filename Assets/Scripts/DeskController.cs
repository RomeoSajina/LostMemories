using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeskController : MonoBehaviour {

	public GameObject[] deskObjects;
	public int sceneIndexForTesting = 1;
	int sceneIndex;

    public Image batteryLife;
    public GameObject canvas;

    public GameObject planePicture;
    
    void Start() {
		Time.timeScale = 1;

        sceneIndex = GameManager.instance.GetSelectedLevel();
        //sceneIndex = PlayerPrefs.GetInt("levelReached", 1) - 1;

		// Postavljanje svega na neaktivno
		for(int i = 0; i < 3; i++){
			deskObjects[i].SetActive(false);
		}

		// Postavljanje oderednih objekata na aktivno
		for(int i = 0; i < sceneIndex; i++){
			deskObjects[i].SetActive(true);
		}


        //Prikazi prethodnu sliku
        //TODO: dodat text + narator
        int index = sceneIndex - 1;

        if (GameManager.instance.ImageExsist(index)) {

            var bytes = GameManager.instance.ReadImage(index);
            var tex = new Texture2D(1, 1);
            tex.LoadImage(bytes);

            Material material = new Material(Shader.Find("Diffuse"));
            material.mainTexture = tex;
            planePicture.GetComponent<Renderer>().material = material;
        }


    }

	void Update() {
		if (Input.GetKeyDown("space")) {
            
            canvas.SetActive(true);
            //StartCoroutine(LoadLevel(sceneIndexForTesting));
            StartCoroutine(LoadLevel(sceneIndex));
            
            //SceneManager.LoadScene(PlayerPrefs.GetInt("levelReached", 1) + 1);
        }
    }

    private IEnumerator LoadLevel(int level) {
        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        float perc = 0.01f;
        while (!async.isDone) {
            yield return null;
            //perc = Mathf.Lerp(perc, async.progress, 0.1f); ili perc = async.progress;
            perc = async.progress;
            batteryLife.fillAmount = perc;
        }
        async.allowSceneActivation = true;
    }
  

}
