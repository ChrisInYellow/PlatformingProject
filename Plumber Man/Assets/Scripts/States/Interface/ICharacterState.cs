using UnityEngine;

namespace States
{

    public interface ICharacterState
    {
        ICharacterState Jump(float input);
        ICharacterState Move(float input);

        ICharacterState Update(Transform transform, Rigidbody2D rb); 
    }
}
