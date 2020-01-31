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

    private Rigidbody zombieRigidBody = null;
    private Animator zombieAnimator = null;

    // Start is called before the first frame update
    void Start()
    {
        zombieRigidBody = GetComponent<Rigidbody>();
        zombieAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (zombieRigidBody.velocity.magnitude < maxVelocity)
        {
            zombieRigidBody.AddForce(Vector3.forward * acceleration * Time.fixedDeltaTime, accelerationForceMode);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<EnvironmentalObjects>() != null)
        {
            if (lifeSpan < 0)
            {
                lifeSpan -= col.gameObject.GetComponent<EnvironmentalObjects>().damageValue;
                zombieAnimator.SetTrigger("IsStumbling");
            }
        }
    }
}

