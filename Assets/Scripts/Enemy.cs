using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameManager gm;

    public Transform[] enemyPathPreAlert;
    public Transform[] enemyPathPostAlert;
    public NavMeshAgent agent;

    private Transform[] enemyPath;

    public Transform raycastStart;
    public Transform raycastTarget;
    private Transform alertPosition;

    private Animator anim;

    public int viewDistance;

    private bool isAlerted = false;

    int i = 0;

    private void Start () {
        anim = GetComponent<Animator>();

        enemyPath = enemyPathPreAlert;

        agent = GetComponent<NavMeshAgent>();

        gm = GameManager.instance;
    }

    void Update () {
        if (!isAlerted) {
            HandleMovement();
        }
        HandleRaycasting();
    }

    void HandleMovement () {
        agent.SetDestination(enemyPath[i % 5].position);
        //Debug.Log(enemyPath[i % 5].position);

        var lookPos = enemyPath[i % 5].position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

        if (transform.position.x == enemyPath[i % 5].position.x && transform.position.z == enemyPath[i % 5].position.z) {
            i++;
            agent.isStopped = true;
            anim.SetBool("isStanding", true);
            StartCoroutine(WaitForSeconds(2));
        }
    }

    public void Alert(Transform alert) {
        enemyPath = enemyPathPostAlert;
        isAlerted = true;
        alertPosition = alert;
        agent.isStopped = true;
        StartCoroutine(AlertStart(1));
    }

    public void HandleAlert () {
        anim.SetBool("isStanding", true);

        var lookPos = alertPosition.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 50f);
        StartCoroutine(AlertEnd(1));
    }

    void HandleRaycasting () {
        //Debug.DrawLine(raycastStart.position, raycastTarget.position - transform.position, Color.blue);
        //Debug.Log(raycastTarget.position);

        RaycastHit hit;

        if (Physics.Raycast(raycastStart.position, raycastTarget.position - transform.position, out hit, viewDistance)) {
            //Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Player") {
                //Debug.Log("Player");
                Time.timeScale = 0;
                gm.HandleDeath();
            }
        }
    }

    public IEnumerator AlertStart (int seconds) {
        yield return new WaitForSeconds(seconds);
        HandleAlert();
    }

    IEnumerator AlertEnd (int seconds) {
        yield return new WaitForSeconds(seconds);
        isAlerted = false;
        agent.isStopped = false;
        anim.SetBool("isStanding", false);
    }

    IEnumerator WaitForSeconds (int seconds) {
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false;
        anim.SetBool("isStanding", false);
    }
}
