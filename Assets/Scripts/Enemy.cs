using UnityEngine;
using UnityEngine.AI;

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

        if (transform.position.x == enemyPath[i % 5].position.x && transform.position.z == enemyPath[i % 5].position.z) {
            i++;
        }
    }

    void HandleRaycasting () {
        Debug.DrawLine(raycastStart.position, raycastTarget.position - transform.position, Color.blue);

        RaycastHit hit;

        if (Physics.Raycast(raycastStart.position, raycastTarget.position - transform.position, out hit, viewDistance)) {
            Debug.Log(hit.transform.tag);
        }
    }
}
