using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CMRotateTowards : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1.0f;

    public Vector3 Target { get; set; }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleBodyRotation();
    }

    private void HandleBodyRotation()
    {
        // update body rotation to interpolate to correct rotation.
        Quaternion bodyTargetRotation = Quaternion.FromToRotation(transform.forward, Target - transform.position) * transform.rotation;
        // restrict to y rotation
        Vector3 asEulerClamped = ClampEulerAngles(bodyTargetRotation.eulerAngles, new Vector3(0, bodyTargetRotation.eulerAngles.y, 0));
        bodyTargetRotation = Quaternion.Euler(asEulerClamped);

        float rotationStep = rotationSpeed * Time.deltaTime;
        Quaternion interpolatedRotation = Quaternion.Slerp(transform.rotation, bodyTargetRotation, rotationStep);
        transform.rotation = interpolatedRotation;
    }

    private Quaternion ClampRotation(Quaternion toClamp, Vector3 limits)
    {
        return Quaternion.Euler(ClampEulerAngles(toClamp.eulerAngles, limits));
    }

    Vector3 ClampEulerAngles(Vector3 angles, Vector3 limits)
    {
        return new Vector3(
            ClampEulerAngle(angles.x, limits.x),
            ClampEulerAngle(angles.y, limits.y),
            ClampEulerAngle(angles.z, limits.z));
    }

    float ClampEulerAngle(float angle, float limit)
    {
        if (angle > 180)
            angle -= 360;
        return Mathf.Clamp(angle, -limit, limit);
    }
}
