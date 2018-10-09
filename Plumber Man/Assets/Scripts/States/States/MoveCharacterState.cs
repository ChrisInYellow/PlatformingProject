
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

        public override ICharacterState Jump(float input)
        {
            return new AirborneState(new Vector3(0, input, 0));
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
