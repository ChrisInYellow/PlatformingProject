﻿
using UnityEngine;
using States; 

public class PlayerStateMachine : MonoBehaviour {
    public float movementSpeed;

    private ICharacterState state = new GroundedState();

    private void Start()
    {
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
            state = state.Jump(); 
        }
    }

    public void Movement()
    {
        if(Input.GetButtonDown("Horizontal"))
        {
            state = state.Move(movementSpeed); 
        }

        state = state.Update(transform); 
    }
}
