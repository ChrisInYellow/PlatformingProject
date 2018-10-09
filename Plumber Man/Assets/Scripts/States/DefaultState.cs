
using UnityEngine;

namespace States
{
    public abstract class DefaultState : ICharacterState {
    
        public virtual ICharacterState Jump(float input)
        {
            return this; 
        }

        public virtual ICharacterState Move(float input)
        {
            return this; 
        }

        public virtual ICharacterState Update(Transform transform, Rigidbody2D rb)
        {
            return this; 
        }
    }

}
