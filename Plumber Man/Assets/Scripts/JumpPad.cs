using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;
    public bool allowJump;
    void Start()
    {
        allowJump = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Feet"))
        {
            if (allowJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
            
        }
    }
}
