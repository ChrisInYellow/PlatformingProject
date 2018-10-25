using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShot : MonoBehaviour
{

    public float bulletSpeed = 20f;

    public Rigidbody2D rb;
    GravityGun gravityGun;

    // Use this for initialization
    void Start()
    {
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

        if (col.gameObject.tag == "Wall")
        {
            //rb.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            //rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            
            //StartCoroutine(WaitKinematicWall());
            rb.transform.localRotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = Vector2.zero;
        }
        if (col.gameObject.tag == "Ground")
        {
            rb.velocity = Vector2.zero;
            Debug.Log("Kine");
            rb.transform.localRotation = Quaternion.identity;
            StartCoroutine(WaitKinematic());
            //rb.isKinematic = true;
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
        //if (rb.gameObject.tag == "JumpPad" && gravityGun.shrinking == false)
        //{
        //    Debug.Log("COLLIDED");
        //    rb.transform.GetChild(0).gameObject.SetActive(true);
        //}

    }
    IEnumerator WaitKinematicWall()
    {

        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.05f);
        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.05f);
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

    }
}
