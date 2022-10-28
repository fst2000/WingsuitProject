using UnityEngine;

public interface ICharacterInfo
{
    Animator Animator { get; }
    Rigidbody Rigidbody { get; }
    Transform Transform { get; }
    PlayerInput PlayerInput { get; }
    
}
