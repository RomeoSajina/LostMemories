using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour {
        
    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Alert");
            GameManager.instance.Alert(gameObject.transform);
        }
    }
}