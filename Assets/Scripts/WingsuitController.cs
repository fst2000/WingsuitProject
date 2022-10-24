using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsuitController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform pivotPosition;
    [SerializeField] new Rigidbody rigidbody = new Rigidbody();
    [SerializeField] float torqueSpeed = 3f;
    [SerializeField] float dragForce = 10f;
    PlayerInput playerInput = new PlayerInput();

    Wing wingForward;
    Wing wingBackward;
    Wing wingStabilize;
    Wing[] wings;
    private void Start()
    {
        wingForward = new Wing(-Vector3.forward, new Vector3(0, 1, 0), 1f, 1.5f);
        wingBackward = new Wing(-Vector3.forward, new Vector3(0, -1, 0), 1f, 1.5f);
        wingStabilize = new Wing(Vector3.forward, new Vector3(0, -1, 0), 0.5f, 1f);
        wings = new Wing[] { wingForward, wingBackward, wingStabilize };
    }
    private void Update()
    {
        animator.SetBool("isFlying", true);

        wingBackward.Rotate(new Vector3(playerInput.moveVertical * 10, 0, 0));
        foreach (Wing wing in wings)
        {
            Vector3 globalNormal = transform.TransformDirection(wing.GetLocalNormal());
            Vector3 wingGlobalPosition = transform.TransformPoint(wing.GetLocalPosition());
            Vector3 globalVelocity = rigidbody.GetPointVelocity(wingGlobalPosition);
            rigidbody.AddForceAtPosition(globalNormal * Vector3.Dot(globalNormal, -globalVelocity) * wing.GetSquare() * dragForce, wingGlobalPosition);
        }
    }
}
