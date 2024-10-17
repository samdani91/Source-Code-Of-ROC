using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceFIreScript : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;

    public int maxHealth = 30;
    public int currenHealth;


    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
        currenHealth = maxHealth;
    }

    void Update()
    {
        if (currenHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void startFire()
    {
        anim.SetTrigger("Fire");

        if (audioSource != null && !audioSource.isPlaying) 
        {
            audioSource.Play();  
        }
    }

    public void TakeDamage(int damage)
    {
        currenHealth -= damage;
    }
}
