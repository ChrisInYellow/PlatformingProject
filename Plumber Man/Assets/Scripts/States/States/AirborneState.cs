
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

        public override ICharacterState Move(float input)
        {
            return this;
        }

        public override ICharacterState Update(Transform transform, Rigidbody2D rb)
        {
            rb.velocity = new Vector2(rb.velocity.x, velocity.y);
            //velocity -= GravityManager.Gravity * Time.deltaTime;

            if (transform.position.y <= 0)
                return new GroundedIdleState(); 

            return this; 
        }
    }
}
