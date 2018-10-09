
using UnityEngine;
namespace States
{
    public class GroundedIdleState : DefaultState
    {
        public override ICharacterState Jump(float input)
        {
            return new AirborneState(new Vector3(0, input, 0)); 
        }

        public override ICharacterState Move(float input)
        {
            return new MoveCharacterState(new Vector3
                (input * Input.GetAxisRaw("Horizontal"), 0, 0)); 
        }

        public override ICharacterState Update(Transform transform, Rigidbody2D rb)
        {
            if(transform.position.y >= 2)
            {
                return new AirborneState(new Vector3(0, 10, 0)); 
            }
            return this; 
        }
    }
}

