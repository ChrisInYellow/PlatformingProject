
using UnityEngine;
namespace States
{
    public class AirborneState : DefaultState
    {
        private Vector3 velocity; 
        
        public AirborneState(Vector3 initalVelocity)
        {
            velocity = initalVelocity; 
        }

        public override ICharacterState Jump(float input)
        {
            return this;
        }

        public override ICharacterState Update(Transform transform)
        {
            transform.position += velocity * Time.deltaTime;
            //velocity -= GravityManager.Gravity * Time.deltaTime;

            if (transform.position.y <= 0)
                return new GroundedState(); 

            return this; 
        }
    }
}
