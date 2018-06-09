using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMainController : MonoBehaviour {

	public Camera mainCamera;

	private Animator am;

	void Start() {
		am = mainCamera.GetComponent<Animator>();
	}

	public void Tutorial() {
		am.Play("MenuCameraToTutorial");
	}

	public void MainMenu() {
		am.Play("MenuCameraFromTutorial");
	}
}
