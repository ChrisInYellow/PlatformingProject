using System.Collections;
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
        if(collision.collider.tag == "Player")
        {
            print("Collided with player!");
            playerHealth.health -= damage;

            knockbackDir = new Vector2(collision.gameObject.transform.position.x - transform.position.x, 
                collision.collider.transform.position.y - transform.position.y) * recoil;

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDir, ForceMode2D.Impulse); 
        }
    }
}
