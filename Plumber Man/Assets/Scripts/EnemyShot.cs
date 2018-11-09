using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{

    public float bulletSpeed = 20f;
    public bool used;
    public Rigidbody2D rb;
    GravityGun gravityGun;


    // Use this for initialization
    void Start()
    {
        used = false;
        gravityGun = GameObject.FindGameObjectWithTag("Player").GetComponent<GravityGun>();
    }
    public void Shoot()
    {
        Debug.Log("Toto");
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.2f);
        GetComponent<EnemyCollisionManager>().enabled = false; 
        rb.velocity = transform.right * bulletSpeed;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Should not happen!");

        if (col.gameObject.tag == "Ground" && used == true || col.gameObject.tag == "Wall" && used == true || col.gameObject.tag == "SmallObject" && used == true || col.gameObject.tag == "Enemy" && used == true)
        {
            Debug.Log("Destroooyed");
            Destroy(gameObject);
        }
        
    }

}
