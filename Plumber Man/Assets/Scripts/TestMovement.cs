using UnityEngine;
using System.Collections;

public class TestMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpHeight;
    public float knockbackTime; 

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool grounded;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public SpriteRenderer gunSprite;

    public bool knockedBack; 

    void Update()
    {
        if(!knockedBack)
        {
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);

                StartCoroutine(Jump());
            }

        }

        float x = Input.GetAxisRaw("Horizontal");

        if (!grounded)
        {
            x *= 0.8f;
        }
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);

        }
        else
        {
            StartCoroutine("Knockback", knockbackTime); 
        }

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Ground")
        {
            grounded = true;
        }

    }

    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "Ground")
        {
            Debug.Log("Air");
            grounded = false;
        }

    }

    IEnumerator Jump()
    {


        yield return new WaitForSeconds(0.4f);

    }

    IEnumerator Knockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        knockedBack = false; 
    }
}