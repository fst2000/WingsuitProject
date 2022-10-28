using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerBehaviour : MonoBehaviour, ICharacterInfo
{
    [SerializeField] Animator animator;
    new Rigidbody rigidbody;
    
    PlayerInput playerInput = new PlayerInput();


    public Animator Animator => animator;
    public Rigidbody Rigidbody => rigidbody;
    public Transform Transform => transform;
    public PlayerInput PlayerInput => playerInput;

    private void Start()
    {
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rigidbody.mass = 60;
    }
    
}
