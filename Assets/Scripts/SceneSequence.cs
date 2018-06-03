using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour {

    private GameManager gm;

    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine(ThisSequence());
	}
	
	IEnumerator ThisSequence() {
        gm.ToggleMouse();
        gm.ToggleMovement();

        yield return new WaitForSeconds(5);
        gm.CinematicShot1();

        yield return new WaitForSeconds(5);
        gm.CinematicShot2();

        yield return new WaitForSeconds(5);
        gm.CinematicShot3();

        yield return new WaitForSeconds(5);
        gm.CinematicUI();

        gm.ToggleMovement();
        gm.ToggleMouse();
    }
}
