﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public int damage;
    public float recoil; 
    private HealthManager playerHealth;
    private Vector2 knockbackDir; 

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<HealthManager>(); 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            playerHealth.health -= damage;
            var playerMovement = FindObjectOfType<TestMovement>();
            playerMovement.knockedBack = true; 

            knockbackDir = new Vector2(collision.gameObject.transform.position.x - transform.position.x,
                collision.gameObject.transform.position.y - transform.position.y) * recoil;

            print(knockbackDir);

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDir, ForceMode2D.Impulse);
            Debug.Log(recoil);
        }
    }


}
