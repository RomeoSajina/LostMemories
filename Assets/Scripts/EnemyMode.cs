using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMode : MonoBehaviour {

    public Camera playerCamera;
    public Camera enemyCamera;

    public GameManager gm;

    public Image fadeImage;

    private bool hasTriggered = false;

    private void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update () {
        if (gm.canMove && !playerCamera.enabled)
            StartCoroutine(FadeIn(false));
    }

    void HandleTransitionToPlayer () {
        if (!playerCamera.enabled) {
            playerCamera.enabled = true;
            enemyCamera.enabled = false;
            AudioManager.instance.StopAll();
        }
    }

    void HandleTranstionFromPlayer(){
        gm.canMove = false;

        playerCamera.enabled = false;
        enemyCamera.enabled = true;
        AudioManager.instance.StopAll();
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player") && !hasTriggered) {
            StartCoroutine(FadeIn(true));

            hasTriggered = true;
        }
    }

    //Photo mode transition
    IEnumerator FadeIn (bool transition) {
        for (float i = 0; i <= 1; i += Time.deltaTime) {
            fadeImage.color = new Color(0, 0, 0, i);

            yield return null;

            StartCoroutine(FadeOut(transition));
        }
    }

    IEnumerator FadeOut (bool transition) {
        for (float i = 1; i >= 0; i -= Time.deltaTime) {
            fadeImage.color = new Color(0, 0, 0, i);

            yield return new WaitForSeconds(.25f);
            
            if(transition)
                HandleTranstionFromPlayer();
            else
                HandleTransitionToPlayer();
        }
    }
}
