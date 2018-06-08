using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeskController : MonoBehaviour {

	public GameObject[] deskObjects;
	public int sceneIndexForTesting = 1;

	void Start() {
		int sceneIndex = PlayerPrefs.GetInt("levelReached", 1) - 1;

		// Postavljanje svega na neaktivno
		for(int i = 0; i < 3; i++){
			deskObjects[i].SetActive(false);
		}

		// Postavljanje oderednih objekata na aktivno
		for(int i = 0; i < sceneIndexForTesting; i++){
			deskObjects[i].SetActive(true);
		}
	}
}
