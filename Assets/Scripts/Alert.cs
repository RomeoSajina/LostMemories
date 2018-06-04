using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour {

    private GameManager gm;
        
    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Alert");
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gm.Alert(transform);
            AudioManager.instance.Play("branch_break");
        }
    }
}