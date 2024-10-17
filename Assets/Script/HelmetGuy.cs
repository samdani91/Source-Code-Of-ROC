using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelmetGuy : MonoBehaviour
{
    public int maxHealth = 30;
    public int currenHealth;



    void Start()
    {
        currenHealth = maxHealth;
    }

    void Update()
    {
        if (currenHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currenHealth -= damage;
    }
}
