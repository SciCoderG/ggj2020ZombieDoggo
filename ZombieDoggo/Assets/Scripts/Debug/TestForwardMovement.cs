using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using de.crystalmesh;
[RequireComponent(typeof(Rigidbody))]
public class TestForwardMovement : MonoBehaviour
{
    //[SerializeField]
    //private float acceleration = 20.0f;
    [SerializeField]
    private float speed = 15.0f;

    //private ForceMode accelerationForceMode = ForceMode.VelocityChange;

    //private Rigidbody objectRigidBody = null;
    // Start is called before the first frame update
    void Start()
    {
        //objectRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.forward;

        transform.position += moveDirection * speed * Time.fixedDeltaTime;

        //if(objectRigidBody.velocity.magnitude < maxVelocity)
        //{
        //    objectRigidBody.AddForce(Vector3.forward * acceleration * Time.fixedDeltaTime, 
        //        accelerationForceMode);
        //}
        //objectRigidBody.velocity =
        //        Utilities.ClampVector(
        //            objectRigidBody.velocity, Vector3.forward * maxVelocity);
    }
}
