using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text dialogueText;
    public Dialogue dialogue;

    public GameObject cameraAlice;
    public GameObject cameraEnemy;

    private Queue<string> sentences;

	void Start () {
        sentences = new Queue<string>();
        cameraEnemy.SetActive(true);
        cameraAlice.SetActive(false);
        StartDialogue(dialogue);
        
        Debug.Log("Start");
    }


    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("Start");
        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);

            
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }else if (sentences.Count == 10) {
            cameraAlice.SetActive(true);
            cameraEnemy.SetActive(false);
        }else if (sentences.Count == 7){
            cameraEnemy.SetActive(true);
            cameraAlice.SetActive(false);
        }else if (sentences.Count == 5){
            cameraAlice.SetActive(true);
            cameraEnemy.SetActive(false);
        }else if (sentences.Count == 1){
            cameraEnemy.SetActive(true);
            cameraAlice.SetActive(false);
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        //StopAllCoroutines();
        //StartCoroutine(TypeSentence(sentence));
        Debug.Log(sentence + " " + sentences.Count);
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.09f);
        }
    }

    void EndDialogue() {
        Debug.Log("End");
    }

}
