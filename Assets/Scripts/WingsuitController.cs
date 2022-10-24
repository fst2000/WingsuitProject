using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsuitController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform pivotPosition;
    [SerializeField] new Rigidbody rigidbody = new Rigidbody();
    private void Update()
    {
        Wing wing = new Wing(-Vector3.forward,Vector3.zero, 1.8f, 1.5f);
    }
}
