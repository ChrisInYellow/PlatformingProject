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
        rb.velocity = transform.right * bulletSpeed;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Wall" || col.gameObject.tag == "SmallObject")
        {
            Destroy(gameObject);
        }
        
    }

}
