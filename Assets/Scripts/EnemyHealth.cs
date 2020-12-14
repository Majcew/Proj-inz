using System.Collections;
using Mirror;
using UnityEngine;

public class EnemyHealth : NetworkBehaviour
{
    public float startingHealth = 40f;
    [SyncVar]
    public float currentHealth;
    public float sinkSpeed = 2.5f; //szybkość znikania w podłodze
    //public int scoreValue = 10;
    //nie wiem, czy będziemy dawać punkty, ale to na razie zostawię
    public AudioClip deathClip;

    Animator anim;
    UnityEngine.AI.NavMeshAgent nav;
    AudioSource enemyAudio;
    //ParticleSystem hitParticles;
    //do efektów krwi przy trafieniu, nie ruszałem
    CapsuleCollider capsuleCollider;
    bool isDead;
    void Awake()
    {
        anim = GetComponent<Animator>();
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
            Death();
        }
    }


    void Death(){
        isDead = true;

        capsuleCollider.isTrigger = true;

        //event do zmiany animacji
        anim.SetTrigger("Dead");
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

    private void DisableAllScripts()
    {
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        NetworkBehaviour[] comps2 = GetComponents<NetworkBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = false;
        }
        foreach (NetworkBehaviour c in comps2)
        {
            c.enabled = false;
        }
    }
}
