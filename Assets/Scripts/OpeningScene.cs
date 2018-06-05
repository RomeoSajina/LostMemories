using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScene : MonoBehaviour {

	private Rigidbody rb;

	bool hasEnded = false;

	void Start () {
		rb = GetComponent<Rigidbody>();
		
		GetComponent<Animator>().SetBool("isWalking", true);
		StartCoroutine(OpeningSceneWalk());
	}

	void Update () {
		if(!hasEnded)
			rb.velocity = new Vector3(0, 0, 3.5f);
		else
			rb.velocity = Vector3.zero;
	}

	IEnumerator OpeningSceneWalk (){
		yield return new WaitForSeconds(20);

		hasEnded = true;
    }
}
