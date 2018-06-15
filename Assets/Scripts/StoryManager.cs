using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {

    public Text dialogueText;
    public Dialogue dialogue;
    public int level = 0;

    private Queue<string> sentences;

    void Start(){
        sentences = new Queue<string>();
        StartDialogue(dialogue);

        Debug.Log("Start");
    }


    public void StartDialogue(Dialogue dialogue){
        
        Debug.Log("Start");
        sentences.Clear();

        if(level == 2) {
            foreach (string sentence in dialogue.level2){
                sentences.Enqueue(sentence);
            }
        }else if (level == 3){
            foreach (string sentence in dialogue.level3){
                sentences.Enqueue(sentence);
            }
        }else if (level == 4){
            foreach (string sentence in dialogue.level4){
                sentences.Enqueue(sentence);
            }
        }

        //DisplayNextSentence();
        StartCoroutine(Sequence());
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        //StopAllCoroutines();
        //StartCoroutine(TypeSentence(sentence));
        Debug.Log(sentence + " " + sentences.Count);
    }

    IEnumerator TypeSentence(string sentence){
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.09f);
        }
    }

    IEnumerator Sequence(){
        yield return new WaitForSeconds(4);
        DisplayNextSentence();
        StartCoroutine(Sequence());
    }

    void EndDialogue(){
        Debug.Log("End");
    }
}
