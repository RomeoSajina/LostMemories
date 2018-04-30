//Original scripting by Aidan Lawrence
using UnityEngine;
using System.Collections;

public class WaterSplash : MonoBehaviour 
{
    public AudioClip[] waterSplashes;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "water")
        {
            AudioSource.PlayClipAtPoint(waterSplashes[Random.Range(0, waterSplashes.Length)], gameObject.transform.position);
        }
    }
}
