using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMother : MonoBehaviour {

    public GameObject gameOver;

    public NavMeshAgent agent;

    public Transform firePlaceLocation;
    public Transform raycastStart;
    public Transform raycastTarget;

    private Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
        anim.SetBool("isStanding", true);
    }

    private void Update () {
        HandleRaycasting();
    }

    void HandleRaycasting () {
        Debug.DrawLine(raycastStart.position, raycastTarget.position - transform.position, Color.blue);
        //Debug.Log(raycastTarget.position);

        RaycastHit hit;

        if (Physics.Raycast(raycastStart.position, raycastTarget.position - transform.position, out hit, 5)) {
            //Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Player") {
                Debug.Log("Player");
                Time.timeScale = 0;
                gameOver.SetActive(true);
            }
        }
    }

    public void HandleAlert () {
        agent.SetDestination(firePlaceLocation.position);
        /*Vector3 targetPostition = new Vector3(firePlaceLocation.position.x,
                                       this.transform.position.y,
                                       firePlaceLocation.position.z);
        this.transform.LookAt(targetPostition);*/
        anim.SetBool("isStanding", false);
    }

    public IEnumerator StartAlert (int seconds) {
        yield return new WaitForSeconds(seconds);
        HandleAlert();
    }
}
