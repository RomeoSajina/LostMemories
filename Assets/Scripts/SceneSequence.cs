using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSequence : MonoBehaviour {

    private GameManager gm;

    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine(ThisSequence());
        //Ako se želi play-at samo jedan zvuk onda moze ovako + treba podesiti vremena cca trebaju biti: 4, 3, 1.5, 2
        //StartCoroutine(StartStory());
    }

        /*
        IEnumerator StartStory() {
            yield return new WaitForSeconds(1);
            AudioManager.instance.PlayIntro(SceneManager.GetActiveScene().buildIndex - 1);
        }
        */

        IEnumerator ThisSequence() {
        gm.ToggleMouse();
        gm.ToggleMovement();

        AudioManager.instance.PlayNarrator("green_forest_intro_1");
        yield return new WaitForSeconds(5);
        gm.CinematicShot1();

        AudioManager.instance.PlayNarrator("green_forest_intro_2");
        yield return new WaitForSeconds(5);
        gm.CinematicShot2();

        AudioManager.instance.PlayNarrator("green_forest_intro_3");
        yield return new WaitForSeconds(5);
        gm.CinematicShot3();

        AudioManager.instance.PlayNarrator("green_forest_intro_4");
        yield return new WaitForSeconds(5);
        gm.CinematicUI();

        gm.ToggleMovement();
        gm.ToggleMouse();
    }
}
