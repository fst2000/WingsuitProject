using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WalkController : MonoBehaviour
{
    [SerializeField]  Animator animator = new Animator();
    [SerializeField]  new Camera camera = new Camera();
    [SerializeField] Rigidbody rb = new Rigidbody();
    [SerializeField] float walkSpeed = 2;
    void Update()
    {
        Vector3 moveInput = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1);
        Vector3 moveDirection = new Vector3(camera.transform.forward.x,0,camera.transform.forward.z);
        Vector3 velocity =  Quaternion.LookRotation(moveDirection, Vector3.up) * moveInput;
        rb.velocity = new Vector3(velocity.x * walkSpeed,rb.velocity.y,velocity.z * walkSpeed);
        if (velocity != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
            transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        

    }
}
