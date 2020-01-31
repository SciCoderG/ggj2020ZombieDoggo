using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovementScript : MonoBehaviour
{
    [SerializeField]
    private float acceleration = 20.0f;
    [SerializeField]
    private float maxVelocity = 15.0f;
    [SerializeField]
    private float lifeSpan = 25.0f;

    private ForceMode accelerationForceMode = ForceMode.VelocityChange;

    private Rigidbody objectRigidBody = null;

    // Start is called before the first frame update
    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objectRigidBody.velocity.magnitude < maxVelocity)
        {
            objectRigidBody.AddForce(Vector3.forward * acceleration * Time.fixedDeltaTime, accelerationForceMode);
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if(col.GetComponent<Environment>() != null)
        {
              //lifeSpan -= col.GameObject.GetComponent<Environment>();
        }
    }
}

