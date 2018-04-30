//Original scripting by Aidan Lawrence
using UnityEngine;
using System.Collections;

public class TreeFall : MonoBehaviour {

	public AudioClip treeFallSound;
	bool playedFX = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.transform.GetComponentInChildren<Animation>().Play();
            GameObject.FindGameObjectWithTag("Transition").GetComponent<Transition>().fadeOut = true;
			if(!playedFX)
			{
				playedFX = true;
				AudioSource.PlayClipAtPoint(treeFallSound, gameObject.transform.position);
			}

        }
            
    }
}
