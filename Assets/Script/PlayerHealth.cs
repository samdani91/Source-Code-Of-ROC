using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currenHealth;

    public HealthBarScript healthbar;
    Animator animator;


    void Start()
    {
        currenHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (currenHealth <= 0)
        {
            animator.SetTrigger("dead");
            StartCoroutine(ReloadSceneWithDelay());
        }
    }

    private IEnumerator ReloadSceneWithDelay()
    {

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void TakeDamage(int damage)
    {
        currenHealth -= damage;
        healthbar.SetHealth(currenHealth);
    }

    public void AddHealth(int health)
    {
        currenHealth += health;
        if(currenHealth > maxHealth)
        {
            currenHealth = maxHealth;
        }
        healthbar.SetHealth(currenHealth);
    }
}
