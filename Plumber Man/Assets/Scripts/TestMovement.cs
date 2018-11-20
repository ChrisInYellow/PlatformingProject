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
    // Use this for initialization
    void Start()
    {


    }


    // Update is called once per frame
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
        //if (Input.GetKeyDown(KeyCode.W))

        //if (Input.GetKey(KeyCode.D))
        //{

        //    //sprite.flipX = false;

        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

        //    //sprite.flipX = true;

        //}

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Ground")
        {
            // Debug.Log("Ground");
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