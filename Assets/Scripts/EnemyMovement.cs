using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    Transform player;
    Health health;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
/*    void Awake()
    {
        //szukanie gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = player.GetComponent<Health>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }*/

    // Update is called once per frame
    void Update()
    {
        //szukanie gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = player.GetComponent<Health>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (enemyHealth.currentHealth > 0 && health.health > 0)
        {
            nav.speed = speed;
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
