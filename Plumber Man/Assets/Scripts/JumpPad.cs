using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;

    private void Start()
    {
            
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Feet"))
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
    }
}
