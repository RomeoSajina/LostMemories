using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMode : MonoBehaviour {

    public Camera playerCamera;
    public Camera enemyCamera;

    public GameManager gm;

    private void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update () {
        if (gm.canMove)
            HandleTransitionToPlayer();
    }

    void HandleTransitionToPlayer () {
        playerCamera.enabled = true;
        enemyCamera.enabled = false;
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            gm.canMove = false;

            playerCamera.enabled = false;
            enemyCamera.enabled = true;
        }
    }
}
