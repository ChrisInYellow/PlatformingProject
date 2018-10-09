﻿
using UnityEngine;
using States; 

public class PlayerStateMachine : MonoBehaviour {
    public float movementSpeed;
    public float jumpingSpeed; 

    private ICharacterState state = new GroundedIdleState();
    private Rigidbody2D rb; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    public void Update()
    {
        Jump();
        Movement(); 
    }

    public void Jump()
    {
       if(Input.GetButtonDown("Jump"))
        {
            state = state.Jump(jumpingSpeed); 
        }
    }

    public void Movement()
    {
        if(Input.GetButtonDown("Horizontal"))
        {
            state = state.Move(movementSpeed); 
        }

        state = state.Update(transform, rb); 
    }
}