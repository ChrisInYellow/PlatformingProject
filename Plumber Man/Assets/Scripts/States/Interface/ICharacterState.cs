using UnityEngine;

namespace States
{

    public interface ICharacterState
    {
        ICharacterState Jump();
        ICharacterState Move(float input);

        ICharacterState Update(Transform transform); 
    }
}
