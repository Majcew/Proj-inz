using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : NetworkBehaviour
{
    [SerializeField]
    private float speed;
    Transform playerTransform;
    GameObject player;
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

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            health = GameManager.GetPlayerHealth(player.GetComponent<NetworkIdentity>().netId.ToString());
        }
        enemyHealth = GameManager.GetEnemyHealth(this.netId.ToString());
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (playerTransform != null && health != null && enemyHealth != null && nav != null)
        {
            if (enemyHealth.currentHealth > 0 && health.health > 0)
            {
                nav.enabled = true;
                nav.speed = speed;
                nav.SetDestination(playerTransform.position);
            }
            else
            {
                nav.enabled = false;
            }
        }
        else
        {
            Debug.Log("Sth is null in Enemy Movemet");
        }
        
    }
}
