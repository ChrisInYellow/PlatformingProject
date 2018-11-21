using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShot : MonoBehaviour
{

    public float bulletSpeed = 20f;

    public Rigidbody2D rb;
    GravityGun gravityGun;
    TestMovement movement;
    RotateGun rotateGun;
    // Use this for initialization
    void Start()
    {
        gravityGun = GameObject.FindGameObjectWithTag("Player").GetComponent<GravityGun>();
        movement = GameObject.FindGameObjectWithTag("Feet").GetComponent<TestMovement>();
        rotateGun = GameObject.FindGameObjectWithTag("Gun").GetComponent<RotateGun>();
    }
    public void Shoot()
    {
        GetComponent<BoxCollider2D>().enabled = true;

        if (rotateGun.facingRight)
        {
            rb.velocity = transform.right * bulletSpeed;
        }
        else if (!rotateGun.facingRight)
        {
            rb.velocity = transform.right * bulletSpeed * -1;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            if (movement.sprite.flipX == true)
            {
                rb.transform.localRotation = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                rb.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (col.gameObject.tag == "Wall")
        {
            rb.velocity = Vector2.zero;
            rb.transform.localRotation = Quaternion.identity;
            StartCoroutine(WaitKinematic());

        }

        if(col.gameObject.tag == "LargeObject")
        {
            Destroy(gameObject); 
        }
    }
    IEnumerator WaitKinematic()
    {
        
        
        yield return new WaitForSeconds(0.4f);
        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return null;
    }
    IEnumerator WaitKinematicWall()
    {
        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.05f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.05f);
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        

    }
}
