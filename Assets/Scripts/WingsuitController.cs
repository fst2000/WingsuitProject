using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Rendering;

public class WingsuitController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform pivotPosition;
    new Rigidbody rigidbody;
    TrailRenderer trailRenderer;
    [SerializeField] float backWingRotationAngle = 15f;
    [SerializeField] float forwardWingRotationAngle = 3f;
    [SerializeField] float stabilizeWingRotationAngle = 5f;
    [SerializeField] float dragForce = 10f;
    [SerializeField] float angularDrag = 2f;
    [SerializeField] float boneRotateIntencity = 0.05f;
    [SerializeField] float boneRotateSpeed = 15f;
    [SerializeField] Transform boneArmL;
    [SerializeField] Transform boneArmR;
    [SerializeField] Transform boneHipL;
    [SerializeField] Transform boneHipR;
    Transform[] rotatedBones;
    PlayerInput playerInput = new PlayerInput();

    Wing wingLeft = new Wing(Vector3.ClampMagnitude(new Vector3(3,-3,-10),1), new Vector3(-0.5f, 1, 0), 1f, 1f);
    Wing wingRight = new Wing(Vector3.ClampMagnitude(new Vector3(-3, -3, -10), 1), new Vector3(0.5f, 1, 0), 1f, 1f);
    Wing wingBack = new Wing(Vector3.ClampMagnitude(new Vector3(0, -3, -10), 1), new Vector3(0, 0, 0), 0.5f, 1f);
    Wing wingStabilize = new Wing(Vector3.right, new Vector3(0, 0.2f, 0), 0.3f, 1f);
    Wing[] wings;
    private void Start()
    {
        rotatedBones = new Transform[] {boneArmL,boneArmR,boneHipL,boneHipR };

        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rigidbody.angularDrag = angularDrag;
        rigidbody.mass = 60;
        wings = new Wing[] { wingLeft, wingRight , wingBack, wingStabilize };
        transform.rotation = Quaternion.Euler(90,0,0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.TransformPoint(wingLeft.GetLocalPosition()), transform.TransformPoint(wingLeft.GetLocalPosition() + wingLeft.GetLocalNormal()));
        Gizmos.DrawLine(transform.TransformPoint(wingRight.GetLocalPosition()), transform.TransformPoint(wingRight.GetLocalPosition() + wingRight.GetLocalNormal()));
        Gizmos.DrawLine(transform.TransformPoint(wingBack.GetLocalPosition()), transform.TransformPoint(wingBack.GetLocalPosition() + wingBack.GetLocalNormal()));
        Gizmos.DrawLine(transform.TransformPoint(wingStabilize.GetLocalPosition()), transform.TransformPoint(wingStabilize.GetLocalPosition() + wingStabilize.GetLocalNormal()));

    }
    private void Update()
    {
        animator.SetBool("isFlying", true);
        animator.SetFloat("flyVertical", -playerInput.moveVertical);
        animator.SetFloat("flyHorizontal", playerInput.moveHorizontal);
        wingLeft.UpdateRotation(new Vector3(-playerInput.moveHorizontal * forwardWingRotationAngle, 0, 0));
        wingRight.UpdateRotation(new Vector3(playerInput.moveHorizontal * forwardWingRotationAngle, 0, 0));
        wingBack.UpdateRotation(new Vector3(-playerInput.moveVertical * backWingRotationAngle, 0, 0));
        wingStabilize.UpdateRotation(new Vector3(0, -playerInput.moveHorizontal * stabilizeWingRotationAngle, 0));

        foreach (Wing wing in wings)
        {
            
            Vector3 globalNormal = transform.TransformDirection(wing.GetLocalNormal());
            Vector3 wingGlobalPosition = transform.TransformPoint(wing.GetLocalPosition());
            Vector3 globalVelocity = rigidbody.GetPointVelocity(wingGlobalPosition);
            rigidbody.AddForceAtPosition(globalNormal * Vector3.Dot(globalNormal, -globalVelocity) * wing.GetSquare() * globalVelocity.magnitude * dragForce, wingGlobalPosition);
        }
    }
    void LateUpdate()
    {
        float velocityMagnitude = Vector3.Magnitude(rigidbody.velocity);
        foreach (Transform bone in rotatedBones)
        {
            Quaternion rotation = bone.localRotation;
            float rotateAngle = Mathf.PerlinNoise(Time.time * boneRotateSpeed * Vector3.Magnitude(rigidbody.velocity), Vector3.Magnitude(bone.localPosition) * 10) * boneRotateIntencity * Mathf.PerlinNoise(0,Time.time) * Mathf.Clamp(Vector3.Magnitude(rigidbody.velocity) * 0.01f,0f,1f);
            bone.localRotation = Quaternion.Euler(new Vector3(rotateAngle,0,0)) * rotation;
        }
    }
}
