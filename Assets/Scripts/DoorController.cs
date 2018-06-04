using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    private Animator animator;

    bool doorOpened = false;

    private bool playerInRange = false;

	// Use this for initialization
	void Start () {

        //animator = GetComponent<Animator>();

        var g = transform.Find("Doors");
        if (g == null)
            g = transform.Find("Door");

        animator = g.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider collider){

        if (collider.tag.Equals("Player")){
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider collider){

        if (collider.tag.Equals("Player")){
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update () {

        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !tag.Equals("FixedDoor")){
            HandleDoorInteraction();
        }

	}

    public void HandleDoorInteraction() {
        if (doorOpened)
            animator.SetTrigger("CloseDoor");

        else
            animator.SetTrigger("OpenDoor");

        doorOpened = !doorOpened;

        AudioManager.instance.Stop("door");
        AudioManager.instance.Play("door");
    }

}
