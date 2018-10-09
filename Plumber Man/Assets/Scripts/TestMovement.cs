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
    

    // Use this for initialization
    void Start()
    {
        
        sprite = GetComponent<SpriteRenderer>();
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
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                
                StartCoroutine(Jump());
            }

        }

        if (Input.GetKey(KeyCode.D))
        {


            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            
            sprite.flipX = false;

        }
        else
        {
            
        }



        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            
            sprite.flipX = true;
            
        }
        else
        {
            
        }


    }

    void OnTriggerStay2D(Collider2D target)
    {
        if (target.tag == "Ground")
        {
            Debug.Log("Ground");
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