using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public static readonly List<string> AllScenes = new List<string>() {"IdleScene", "GreenForest", "FantasyRoom", "FallenSchool", "ColdSnow" };
    private static readonly string IMAGE_NAME = "/Images/SavedScreen_";

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
        Time.timeScale = 0f;
        canMove = false;
        AudioManager.instance.PlayNarrator(AudioManager.intros[PlayerPrefs.GetInt("levelReached", 1) + 1]);
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

    public void ToggleMouse() {
        canMouseLook = !canMouseLook;
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


    public void SaveImage(byte[] bytes, int level = -1) {
        if (level == -1)
            level = GetReachedLevel();

        string path = Application.dataPath + "/Images";

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        File.WriteAllBytes(Application.dataPath + IMAGE_NAME + level + ".png", bytes);
    }

    public byte[] ReadImage(int level = -1) {
        if (level == -1)
            level = GetReachedLevel();

        return File.ReadAllBytes(Application.dataPath + IMAGE_NAME + level + ".png");
    }

    public bool ImageExsist(int level) {
        return File.Exists(Application.dataPath + IMAGE_NAME + level + ".png");
    }
}
