using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour {

    private Scene scene;
    private GameManager gm;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Start() {
        Time.timeScale = 1f;
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        //Freez time; Slow motion
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() {
        //Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        //TODO: Dodati sceneIndex umjesto hard-kodiranja.
        SceneManager.LoadScene("IdleScene");
    }

    public void QuitGame(){
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void  Restart() {
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
    }

    public void NextLevel(){
        int index = scene.buildIndex + 1;
        //gm.ToggleMovement();

        AudioManager.instance.StopAll(true);

        PlayerPrefs.SetInt("levelReached", scene.buildIndex);

        SceneManager.LoadScene(index);
    }

    public void SkipStory(){
        int index = scene.buildIndex + 1;
        SceneManager.LoadScene(index);
    }
}
