using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour {

    public GameObject fire;
    public EnemyMother enemyMother;

    private void OnTriggerStay (Collider other) {
        if (other.CompareTag("Player") && Input.GetKeyDown("e")) {
            Debug.Log("Vatra ugasena/upaljena");
            fire.SetActive(false);

            StartCoroutine(enemyMother.StartAlert(2));
        }
    }
}
