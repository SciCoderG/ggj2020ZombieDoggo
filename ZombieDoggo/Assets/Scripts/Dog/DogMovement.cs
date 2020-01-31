using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DogMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 acceleration = new Vector2(15.0f, 20.0f);
    [Tooltip("")]
    [SerializeField]
    private Vector2 maxVelocity = new Vector2(30.0f, 20.0f);

    private Rigidbody dogRigidBody = null;
    private ForceMode accelerationForceMode = ForceMode.VelocityChange;

    // Start is called before the first frame update
    void Awake()
    {
        dogRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessHorizontalInput();
        ProcessVerticalInput();
    }

    private void ProcessHorizontalInput()
    {
        Vector3 horInputVec = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        if (dogRigidBody.velocity.x < maxVelocity.x)
        {
            dogRigidBody.AddForce(horInputVec * acceleration.x * Time.fixedDeltaTime, accelerationForceMode);
        }
    }

    private void ProcessVerticalInput()
    {
        Vector3 vertInputVec = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
        if (dogRigidBody.velocity.z < maxVelocity.y)
        {
            dogRigidBody.AddForce(vertInputVec * acceleration.y * Time.fixedDeltaTime, accelerationForceMode);
        }
    }
}
