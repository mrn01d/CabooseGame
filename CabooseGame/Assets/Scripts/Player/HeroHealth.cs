﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class HeroHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int maxHealth;
    public Slider healthSlider;
    public Image damageImage;
    //public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    Rigidbody playerRBody;


    Animator anim;
    //AudioSource playerAudio;
    PlayerController playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
    int swingID;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
       // playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerController> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
        playerRBody = GetComponent<Rigidbody>();
        maxHealth = startingHealth;
        
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
            healthSlider.value = currentHealth;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        
    }



    public void TakeDamage(int amount, int newSwingID)
    {
        if (isDead)
            return;

        if (newSwingID != swingID)
        {
            currentHealth -= amount;
            anim.SetTrigger("GetHit1Trigger");
            swingID = newSwingID;
            damaged = true;
            
        }

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death ()
    {
        isDead = true;

        //playerShooting.DisableEffects ();

        //anim.SetTrigger ("Die");

        //playerAudio.clip = deathClip;
        // playerAudio.Play ();
        playerRBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
        playerRBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        playerMovement.heroDeath();
        playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
