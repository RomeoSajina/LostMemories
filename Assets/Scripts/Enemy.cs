using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public Transform[] enemyPath;
    public NavMeshAgent agent;
    int i = 0;

	void Update () {
        agent.SetDestination(enemyPath[i % 5].position);

        if(transform.position.x == enemyPath[i%5].position.x && transform.position.z == enemyPath[i % 5].position.z) {
            i++;
        }
	}
}
