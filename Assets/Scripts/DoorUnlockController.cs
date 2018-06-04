using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlockController : MonoBehaviour {

    //bool playerInRange = false;

    private DoorController doorController;
    private EnemyController enemyController = null;

	// Use this for initialization
	void Start () {
        doorController = GetComponent<DoorController>();
	}

    void OnTriggerEnter(Collider collider) {

        if (collider.tag.Equals("Enemy")){
            enemyController = collider.GetComponent<EnemyController>();
            //playerInRange = true;
        }
    }

    void OnTriggerExit(Collider collider) {

        if (collider.tag.Equals("Enemy")) {
            enemyController = null;
            //playerInRange = false;

        }
    }


    // Update is called once per frame
    void Update() {

        if (enemyController != null && Input.GetKeyDown(KeyCode.E) && tag.Equals("FixedDoor")) {
            tag = "Untagged";

            //TODO: emit event za prebacivanje na drugog lika
            doorController.HandleDoorInteraction();
            enemyController.MoveToStaringPosition();
        }

    }
}
