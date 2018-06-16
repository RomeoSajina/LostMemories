using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMainController : MonoBehaviour {

	public Camera mainCamera;

	private Animator am;

    public GameObject[] levels;

    void Start() {
		am = mainCamera.GetComponent<Animator>();
        //Debug.Log("Level reached: "+PlayerPrefs.GetInt("levelReached", 1));

        for (int i = 3; i >= PlayerPrefs.GetInt("levelReached", 1); i--){
            levels[i].GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, .1f);
            levels[i].GetComponent<ClickableGameObject>().isClickable = false;
        }
        AudioManager.instance.Play("background");
    }

	public void Tutorial() {
		am.Play("MenuCameraToTutorial");
	}

	public void Levels() {
		am.Play("MenuCameraToLevels");
	}

	public void MainMenuT() {
		am.Play("MenuCameraFromTutorial");
	}

	public void MainMenuL() {
		am.Play("MenuCameraFromLevels");
	}

    public void Quit(){
        Application.Quit();
    }
    
    public void SelectLevel(int index){
        int level = PlayerPrefs.GetInt("levelReached", 1);

        if(index <= level) {

            //Ovaj dio tu smo rekli da nije dobar, ali tako cemo jednostavno rješit kompatibilnost sa ostalim skriptama
            if (index == 1) {
                PlayerPrefs.SetInt("levelReached", index);

            } else {
                
                PlayerPrefs.SetInt("levelReached", index - 1); // -1 jer je taj selektirani level kao novi sada (kao da smo tek prešli level prije)
                index = 5; //Pokreni story-scene
            }

            StartCoroutine(LoadLevel(index));
            AudioManager.instance.StopAll();
        }
    }

    private IEnumerator LoadLevel(int level) {
        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        float perc = 0.01f;
        while (!async.isDone) {
            yield return null;
            //perc = Mathf.Lerp(perc, async.progress, 0.1f); ili perc = async.progress;
            perc = async.progress;
            //batteryLife.fillAmount = perc;
        }
        async.allowSceneActivation = true;
    }
}
