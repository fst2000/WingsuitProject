using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsuitController : MonoBehaviour
{
    [SerializeField] Animator animator = new Animator();
    [SerializeField] new Rigidbody rigidbody = new Rigidbody();
    [SerializeField] float forwardForce = 100;
    [SerializeField] float torqueSpeed = 5;
    private void Update()
    {
        animator.SetBool("isFlying", true);
        Vector3 input = new Vector3(Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal"));
        Quaternion lookRotation = Quaternion.LookRotation(rigidbody.velocity) * Quaternion.Euler(90,0,0);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.5f);
    }
}
