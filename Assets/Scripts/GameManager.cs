using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private Enemy enemy;

    public GameObject deathUI;
    public GameObject winUI;

    public bool canMove = true;

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
}
