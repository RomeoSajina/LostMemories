using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void HandleDeath () {
        //deathUI = GameObject.FindGameObjectWithTag("lose");
        deathUI.SetActive(true);
    }

    public void HandleWin () {
        //winUI = GameObject.FindGameObjectWithTag("win");
        winUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Alert (Transform alert) {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        enemy.Alert(alert);
    }

    public void ToggleMovement () {
        canMove = !canMove;
    }

    public void ToggleMouse(){
        canMouseLook = !canMouseLook;
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
