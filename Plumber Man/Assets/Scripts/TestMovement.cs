using UnityEngine;
using System.Collections;

public class TestMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool grounded;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        
        
    }

    void fixedUpdate()
    {



    }
    // Update is called once per frame
    void Update()
    {



        //if (Input.GetKeyDown(KeyCode.W))
        if(Input.GetButtonDown("Jump"))
        {
            if (grounded == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                
                StartCoroutine(Jump());
            }

        }

        if (Input.GetKey(KeyCode.D))
        {


           rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            
            sprite.flipX = false;

        }
        else
        {
            
        }



        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            
            sprite.flipX = true;
            
        }
        else
        {
            
        }


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
}