using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[System.Serializable]
public class WalkController : ICharacterController
{
    

    [SerializeField]  Animator animator = new Animator();
    [SerializeField]  new Camera camera = new Camera();
    [SerializeField] Rigidbody rigidbody = new Rigidbody();
    [SerializeField] float walkSpeed = 2;
    PlayerInput playerInput = new PlayerInput();
    ICharacterInfo characterInfo;
    public void Initialize(ICharacterInfo characterInfo)
    {
        this.characterInfo = characterInfo;
    }
    public void LateUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnDrawGizmos()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        Vector3 moveInput = Vector3.ClampMagnitude(new Vector3(playerInput.MoveHorizontal, 0, playerInput.MoveVertical), 1);
        Vector3 moveDirection = new Vector3(camera.transform.forward.x,0,camera.transform.forward.z);
        Vector3 velocity =  Quaternion.LookRotation(moveDirection, Vector3.up) * moveInput;
        rigidbody.velocity = new Vector3(velocity.x * walkSpeed,rigidbody.velocity.y,velocity.z * walkSpeed);
        if (velocity != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
            characterInfo.Transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        

    }

    void ICharacterController.Update()
    {
        throw new System.NotImplementedException();
    }
}
