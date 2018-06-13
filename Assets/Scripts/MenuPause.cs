using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour {

    //private GameManager gm;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Start() {
        Time.timeScale = 1f;
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
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        //SceneManager.LoadScene("IdleScene");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame(){
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    // Pozivanje na kraju levela
    public void NextLevel(){
        AudioManager.instance.StopAll(true);

        int index = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("levelReached", index);

        SceneManager.LoadScene(5);
    }

    // Pozivanje na Story levelu
    public void SkipStory(){
        SceneManager.LoadScene(PlayerPrefs.GetInt("levelReached", 1) + 1);
    }
}
