
using UnityEngine;
namespace States
{
    public class GroundedState : DefaultState
    {
        public override ICharacterState Jump()
        {
            return new AirborneState(new Vector3(0, 2, 0)); 
        }

        public override ICharacterState Move(float input)
        {
            return new MoveCharacterState(new Vector3
                (input * Input.GetAxisRaw("Horizontal"), 0, 0)); 
        }

        public override ICharacterState Update(Transform transform)
        {
            if(transform.position.y >= 2)
            {
                return new AirborneState(new Vector3(0, 2, 0)); 
            }
            return this; 
        }
    }
}

