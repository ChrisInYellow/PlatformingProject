
using UnityEngine;
namespace States
{

public class MoveCharacterState : DefaultState
{
        private Vector3 velocity; 
        public MoveCharacterState(Vector3 InitialVelocity)
        {
            velocity = InitialVelocity; 
        }

        public override ICharacterState Jump()
        {
            return this; 
        }

        public override ICharacterState Update(Transform transform)
        {
            transform.position += velocity * Time.deltaTime;

            if (Input.GetButtonUp("Horizontal"))
                return new GroundedState(); 

            return this; 
        }
    }

}
