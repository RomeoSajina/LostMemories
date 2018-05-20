using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorController : MonoBehaviour {

    public bool isCollected { get; private set; }

	// Use this for initialization
	void Start () {
        isCollected = false;
	}
	
    private void OnTriggerEnter(Collider other){

        if (!isCollected && other.tag.Equals("Player")){
            Destroy(gameObject);
            isCollected = true;
        }

    }

}
