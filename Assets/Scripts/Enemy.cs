using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Transform[] enemyPath;
    public NavMeshAgent agent;

    public Transform raycastStart;
    public Transform raycastTarget;

    public int viewDistance;

    int i = 0;

	void Update () {
        HandleMovement();
        HandleRaycasting();
    }

    void HandleMovement () {
        agent.SetDestination(enemyPath[i % 5].position);

        var lookPos = enemyPath[i % 5].position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

        if (transform.position.x == enemyPath[i % 5].position.x && transform.position.z == enemyPath[i % 5].position.z) {
            i++;
            agent.isStopped = true;
            StartCoroutine(waitForSeconds(2));
        }
    }

    void HandleRaycasting () {
        Debug.DrawLine(raycastStart.position, raycastTarget.position - transform.position, Color.blue);

        RaycastHit hit;

        if (Physics.Raycast(raycastStart.position, raycastTarget.position - transform.position, out hit, viewDistance)) {
            Debug.Log(hit.transform.tag);
        }
    }

    IEnumerator waitForSeconds (int seconds) {
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false;
    }
}
