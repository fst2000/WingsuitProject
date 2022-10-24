using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsuitController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform pivotPosition;
    [SerializeField] new Rigidbody rigidbody = new Rigidbody();
    [SerializeField] float torqueSpeed = 3f;
    [SerializeField] float upForce = 10f;
    PlayerInput playerInput = new PlayerInput();
    private void Update()
    {
        animator.SetBool("isFlying", true);
        Wing wing = new Wing(-Vector3.forward,Vector3.zero, 1.8f, 1.5f);
        Vector3 globalNormal = transform.TransformDirection(wing.GetLocalNormal());
        rigidbody.AddRelativeTorque(playerInput.moveVertical * torqueSpeed, -playerInput.moveHorizontal * torqueSpeed, 0);
        rigidbody.AddForce(globalNormal * Vector3.Dot(globalNormal, -rigidbody.velocity) * wing.GetSquare() * upForce) ;
    }
}
