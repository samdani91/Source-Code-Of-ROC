using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBottleScript : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Hero").GetComponent<PlayerHealth>();
    }

    public void DestroyWaterBottle()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {

            playerHealth.AddHealth(25);
                DestroyWaterBottle();
            
        }
    }

            
}
