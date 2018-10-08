
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
        return; 
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
