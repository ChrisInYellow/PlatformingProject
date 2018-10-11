using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {
    public int healAmount; 
    private HealthManager playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<HealthManager>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(playerHealth.health<playerHealth.numbofHearts)
            {
                playerHealth.health += healAmount;
                gameObject.SetActive(false);

            }
        }
    }
}
