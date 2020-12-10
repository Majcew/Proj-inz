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
    AudioSource enemyAudio;
    //ParticleSystem hitParticles;
    //do efektów krwi przy trafieniu, nie ruszałem
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        //hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
    }

    
    void Update()
    {
        if(isSinking){
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime );
        }
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
            Death();
        }
    }


    void Death(){
        isDead = true;

        capsuleCollider.isTrigger = true;

        //event do zmiany animacji
        anim.SetTrigger("Dead");
        anim.SetBool("ZoombieDead", true);
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
        GetComponent <UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent <Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy (gameObject, 6f);
    }


}
