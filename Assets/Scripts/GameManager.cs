using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public Enemy enemy;

    public bool canMove = true;

    private void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    public void Alert (Transform alert) {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        enemy.Alert(alert);
    }

    public void ToggleMovement () {
        canMove = !canMove;
    }
}
