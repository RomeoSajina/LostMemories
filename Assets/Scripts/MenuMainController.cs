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

        for(int i = 3; i >= PlayerPrefs.GetInt("levelReached", 1); i--){
            levels[i].GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, .1f);
            levels[i].GetComponent<ClickableGameObject>().isClickable = false;
        }
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
            StartCoroutine(LoadLevel(index));
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
