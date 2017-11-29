﻿using System.Timers;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public PlayerAttack playerAtt;
    EnemyController enemyController;
    enemyWeapon weapon;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    
    public bool isDead;
    bool isSinking;
    int swingID;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        // enemyAudio = GetComponent <AudioSource> ();
        // hitParticles = GetComponentInChildren <ParticleSystem> ();
        enemyController = GetComponent<EnemyController>();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        weapon = GetComponentInChildren<enemyWeapon>();
        if (playerAtt != null)
        {
            Debug.Log("PlayerAttack sucessfully found");
        }
        swingID = 0;
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if (isDead)
        {
            
            //StartSinking();
            if (isSinking)
            {
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }

    }


    public int TakeDamage(int amount, int newSwingID)
    {
        if(isDead)
            return 0;

        // enemyAudio.Play ();
       if (newSwingID!=swingID)
        {
        currentHealth -= amount;
        anim.SetTrigger("GetHit1Trigger");
        swingID = newSwingID;
        }
        
       
        // hitParticles.transform.position = hitPoint;
        // hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death ();
            Debug.Log("Returned 1");
            return 1;

        }
        else
        {
            return 0;
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        enemyController.enemyDeath();
        enemyController.enabled = false;

        weapon.weaponActive = false;
        StartSinking();
        

        /* anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play (); */


    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        //ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
