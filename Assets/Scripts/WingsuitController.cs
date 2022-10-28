using UnityEngine;

[System.Serializable]
public class WingsuitController : ICharacterController
{
    ICharacterInfo characterInfo;
    public void Initialize(ICharacterInfo characterInfo)
    {
        this.characterInfo = characterInfo;
        rotatedBones = new Transform[] { boneArmL, boneArmR, boneHipL, boneHipR };
        wings = new Wing[] { wingLeft, wingRight, wingBack, wingStabilize };
    }
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
    Wing wingLeft = new Wing(Vector3.ClampMagnitude(new Vector3(3, -3, -10), 1), new Vector3(-0.5f, 1, 0), 1f, 1f);
    Wing wingRight = new Wing(Vector3.ClampMagnitude(new Vector3(-3, -3, -10), 1), new Vector3(0.5f, 1, 0), 1f, 1f);
    Wing wingBack = new Wing(Vector3.ClampMagnitude(new Vector3(0, -3, -10), 1), new Vector3(0, 0, 0), 0.5f, 1f);
    Wing wingStabilize = new Wing(Vector3.right, new Vector3(0, 0.2f, 0), 0.3f, 1f);
    Wing[] wings;
    public void OnDrawGizmos()
    {
        if (wings == null) return;

        foreach (Wing wing in wings)
        {
            Matrix4x4 matrixRotation = Matrix4x4.Rotate(characterInfo.Transform.rotation * wing.GetWingRotation());
            Matrix4x4 matrixPosition = Matrix4x4.Translate(characterInfo.Transform.TransformPoint(wing.GetLocalPosition()));
            Gizmos.matrix = matrixPosition * matrixRotation;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(Vector3.zero, Vector3.forward);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, wing.GetSize());
        }

    }
    public void Update()
    {
        characterInfo.Animator.SetBool("isFlying", true);
        characterInfo.Animator.SetFloat("flyVertical", -characterInfo.PlayerInput.MoveVertical);
        characterInfo.Animator.SetFloat("flyHorizontal", characterInfo.PlayerInput.MoveHorizontal);
        wingLeft.UpdateRotation(new Vector3(-characterInfo.PlayerInput.MoveHorizontal * forwardWingRotationAngle, 0, 0));
        wingRight.UpdateRotation(new Vector3(characterInfo.PlayerInput.MoveHorizontal * forwardWingRotationAngle, 0, 0));
        wingBack.UpdateRotation(new Vector3(-characterInfo.PlayerInput.MoveVertical * backWingRotationAngle, 0, 0));
        wingStabilize.UpdateRotation(new Vector3(0, -characterInfo.PlayerInput.MoveHorizontal * stabilizeWingRotationAngle, 0));

        foreach (Wing wing in wings)
        {

            Vector3 globalNormal = characterInfo.Transform.TransformDirection(wing.GetLocalNormal());
            Vector3 wingGlobalPosition = characterInfo.Transform.TransformPoint(wing.GetLocalPosition());
            Vector3 globalVelocity = characterInfo.Rigidbody.GetPointVelocity(wingGlobalPosition);
            characterInfo.Rigidbody.AddForceAtPosition(globalNormal * Vector3.Dot(globalNormal, -globalVelocity) * wing.GetSquare() * globalVelocity.magnitude * dragForce, wingGlobalPosition);
        }
    }
    public void LateUpdate()
    {
        float velocityMagnitude = Vector3.Magnitude(characterInfo.Rigidbody.velocity);
        foreach (Transform bone in rotatedBones)
        {
            Quaternion rotation = bone.localRotation;
            float rotateAngle = Mathf.PerlinNoise(Time.time * boneRotateSpeed * Vector3.Magnitude(characterInfo.Rigidbody.velocity), Vector3.Magnitude(bone.localPosition) * 10) * boneRotateIntencity * Mathf.PerlinNoise(0, Time.time) * Mathf.Clamp(Vector3.Magnitude(characterInfo.Rigidbody.velocity) * 0.01f, 0f, 1f);
            bone.localRotation = Quaternion.Euler(new Vector3(rotateAngle, 0, 0)) * rotation;
        }
    }
}
