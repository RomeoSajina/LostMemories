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

        AudioManager.instance.PlayIntro(1);
        yield return new WaitForSeconds(5);
        gm.CinematicShot1();

        AudioManager.instance.PlayIntro(2);
        yield return new WaitForSeconds(5);
        gm.CinematicShot2();

        AudioManager.instance.PlayIntro(3);
        yield return new WaitForSeconds(5);
        gm.CinematicShot3();

        AudioManager.instance.PlayIntro(4);
        yield return new WaitForSeconds(5);
        gm.CinematicUI();

        gm.ToggleMovement();
        gm.ToggleMouse();
    }
}
