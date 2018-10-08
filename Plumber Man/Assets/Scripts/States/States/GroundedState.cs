
using UnityEngine;
namespace States
{
    public class GroundedState : DefaultState
    {
        public override ICharacterState Jump()
        {
            return this; 
        }

        public override ICharacterState Move(float input)
        {
            return new MoveCharacterState(new Vector3
                (input * Input.GetAxisRaw("Horizontal"), 0, 0)); 
        }

        public override ICharacterState Update(Transform transform)
        {
            return this; 
        }
    }
}

