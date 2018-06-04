    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTrigger : MonoBehaviour {

    public Text hintText;
    public string hint;
    public string narratorSoundName;
    private bool hasTriggered = false;

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag("Player") && !hasTriggered) {

            hasTriggered = true;
            hintText.text = hint;
            AudioManager.instance.PlayNarrator(narratorSoundName);

            StartCoroutine(DisableHint(3));
        }
    }

    public IEnumerator DisableHint (int seconds) {
        yield return new WaitForSeconds(seconds);
        hintText.text = "";
    }

}
