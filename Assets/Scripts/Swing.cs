using System;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public Camera fpscam;
    private Animator animator;
    public float fireRate;
    public float damage;
    float fireTimer;
    public AudioSource audioSource;
    public AudioClip swingSound;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButton("Fire1") && fireTimer > fireRate)
        {
            SwingWeapon();
            fireTimer = 0;
        }
    }

    private void SwingWeapon()
    {
        audioSource.PlayOneShot(swingSound);
        RaycastHit hit;
        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit,0.75f))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            Debug.Log(hit.transform.name);
            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
        animator.SetTrigger("Attack");
    }
}
