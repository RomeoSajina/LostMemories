using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private Enemy enemy;

    public GameObject deathUI;
    public GameObject winUI;

    public GameObject Cam1;
    public GameObject Cam2;
    public GameObject Cam3;
    public GameObject Cinematic;

    public bool canMove = true;
    public bool canMouseLook = true;

    public static GameManager instance;

    private static readonly List<string> scenes = new List<string>() { "GreenForest", "FantasyRoom", "FallenSchool", "ColdSnow" };

    void Awake() { instance = this; }

    public void HandleDeath () {
        //deathUI = GameObject.FindGameObjectWithTag("lose");
        AudioManager.instance.StopAll();
        deathUI.SetActive(true);
    }

    public void HandleWin () {
        //winUI = GameObject.FindGameObjectWithTag("win");
        AudioManager.instance.StopAll();
        winUI.SetActive(true);
        Time.timeScale = 0;
        canMove = false;
        AudioManager.instance.PlayNarrator(AudioManager.intros[GetCurrentLevel()+1]);
    }

    public void Alert (Transform alert) {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        enemy.Alert(alert);
        //Samo za testiranje
        //HandleWin();
    }

    public void ToggleMovement () {
        canMove = !canMove;
    }

    public void ToggleMouse(){
        canMouseLook = !canMouseLook;
    }

    /* Vraća trenutni level - 1 tako da se lakše radi sa array-evima, tj da ne treba uvijek stavljati GetCurrentLevel() - 1 */
    public int GetCurrentLevel() {

        Scene scene = SceneManager.GetActiveScene();

        //Debug.Log("Scene name: " + scene.name);
        int current = scenes.IndexOf(scene.name);

        return current;
    }


    public int GetReachedLevel() {
        return PlayerPrefs.GetInt("levelReached", 1); 
    }

    public void CinematicShot1() {
        Cam2.SetActive(true);
        Cam1.SetActive(false);
    }

    public void CinematicShot2(){
        Cam3.SetActive(true);
        Cam2.SetActive(false);
    }

    public void CinematicShot3(){
        Cam3.SetActive(false);
    }

    public void CinematicUI(){
        Cinematic.SetActive(false);
    }
}
