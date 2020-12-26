﻿using System.Collections;
using Mirror;
using UnityEngine;

public class EnemyHealth : NetworkBehaviour
{
    public float startingHealth = 40f;
    [SyncVar]
    public float currentHealth;
    //public int scoreValue = 10; nie wiem, czy będziemy dawać punkty, ale to na razie zostawię
    public AudioClip deathClip;

    Animator anim;
    NetworkAnimator n_anim;
    UnityEngine.AI.NavMeshAgent nav;
    AudioSource enemyAudio;
    //ParticleSystem hitParticles; do efektów krwi przy trafieniu, nie zaimplementowane
    CapsuleCollider capsuleCollider;
    bool isDead;

    [Header("List of items that the enemy can drop after death")]
    [SerializeField]
    private GameObject[] item_list;

    [Header("List of behaviours to disable after death")]
    [SerializeField]
    private Behaviour[] behaviours;
    void Awake()
    {
        anim = GetComponent<Animator>();
        n_anim = GetComponent<NetworkAnimator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyAudio = GetComponent<AudioSource>();
        //hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
    }

    void Start()
    {
        GameManager.AddEnemyHealth(this.netId.ToString(), this);
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
            return;
        enemyAudio.Play();
        currentHealth -= amount;
        //hitParticles.transform.position = hitPoint;
        //hitParticles.Play();
        if (currentHealth <= 0)
        {
            nav.enabled = false;
            DisableAllScripts();
            RollItem();
            Death();
        }
    }


    void Death(){
        isDead = true;

        capsuleCollider.isTrigger = true;

        //event do zmiany animacji
        n_anim.SetTrigger("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        StartCoroutine(WaitTimeBeforeDisappear(5));
    }
    IEnumerator WaitTimeBeforeDisappear(int sec)
    {
        yield return new WaitForSeconds(sec);
        StartSinking();
    }
    public void StartSinking(){
        Destroy(gameObject, 6f);
    }
    /* Funkcja odpowiedzialna za spawnowanie różnych itemków po śmierci przeciwnika.
       W celu poprawnego działania przedmioty powinny posiadać odpowiednie skrypty od pakietu "Mirror".
       Należy te obiekty także podpiąć do "NetworkManagera" w zakładce "Spawnable Prefabs"
    */
    private void RollItem()
    {
        // Pobieramy pozycję przeciwnika po śmierci
        Vector3 position = transform.position;
        // Będziemy losowali z tablicy o zadanej wymiarze.
        int index = Random.Range(0,item_list.Length);
        if(item_list[index] != null)
        {
            GameObject toRender = Instantiate(item_list[index], position + new Vector3(0.0f, 0.3f, 0.0f), Quaternion.identity);
            NetworkServer.Spawn(toRender);     
        }
    }

    private void DisableAllScripts()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        foreach (Behaviour c in behaviours)
        {
            c.enabled = false;
        }
    }
}
