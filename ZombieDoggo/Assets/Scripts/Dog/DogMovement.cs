using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using de.crystalmesh;

[RequireComponent(typeof(Rigidbody), typeof(CMRotateTowards))]
public class DogMovement : MonoBehaviour
{
    [Tooltip("Acceleration in left/right and forward/back direction")]
    [SerializeField]
    private Vector2 acceleration = new Vector2(15.0f, 20.0f);
    [Tooltip("Maximum movement speed in left/right and forward/back direction")]
    [SerializeField]
    private Vector2 maxVelocity = new Vector2(30.0f, 20.0f);
    [SerializeField]
    float deccelerationSpeed = 2.0f;
    [SerializeField]
    private float slowDownMultiplierOnGrab = 0.5f;
    public Vector2 MaxVelocity { get { return maxVelocity; } set { maxVelocity = value; } }
   
    public bool SlowDownWhileDragging { get; set; }

    private Rigidbody dogRigidBody = null;
    private CMRotateTowards rotateTowardsScript = null;

    [SerializeField]
    private Animator doggoAnimator = null;

  

    void Awake()
    {
        dogRigidBody = GetComponent<Rigidbody>();
        rotateTowardsScript = GetComponent<CMRotateTowards>();
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocityChange = Vector3.zero;
        velocityChange += ProcessHorizontalInput();
        velocityChange += ProcessVerticalInput();
        dogRigidBody.velocity += velocityChange * Time.fixedDeltaTime;

        DampVelocity(velocityChange);
        ClampVelocity();
        UpdateRotation();
        doggoAnimator.SetFloat("velocityX", Mathf.Abs(dogRigidBody.velocity.x));
        doggoAnimator.SetFloat("velocityZ", Mathf.Abs(dogRigidBody.velocity.z));
    }

    private void DampVelocity(Vector3 velocityChange)
    {
        if (velocityChange.sqrMagnitude < 0.2f)
        {
            Vector3 damp = -dogRigidBody.velocity.normalized * deccelerationSpeed * Time.fixedDeltaTime;
            if (Mathf.Abs(damp.x) > Mathf.Abs(dogRigidBody.velocity.x))
                damp.x = 0.0f;
            if (Mathf.Abs(damp.z) > Mathf.Abs(dogRigidBody.velocity.z))
                damp.z = 0.0f;
            damp.y = 0.0f;
            dogRigidBody.velocity += damp;
        }
    }

    private void ClampVelocity()
    {
        float slowValueX = SlowDownWhileDragging ? slowDownMultiplierOnGrab : 1f;
        float slowValueY = SlowDownWhileDragging ? 0.0f : 1f;

        dogRigidBody.velocity = Utilities.ClampVector(
            dogRigidBody.velocity, new Vector3(
                maxVelocity.x * slowValueX,
                10.0f,
                maxVelocity.y * slowValueY));
    }

    private void UpdateRotation()
    {
        Vector3 currentVel = dogRigidBody.velocity;
        if(currentVel.sqrMagnitude > 0.1f)
        {
            rotateTowardsScript.Target = dogRigidBody.transform.position + dogRigidBody.velocity.normalized;
        }
        else
        {
            rotateTowardsScript.Target = dogRigidBody.transform.position + dogRigidBody.transform.forward;
        }
    }

    private Vector3 ProcessHorizontalInput()
    {
        Vector3 horInputVec = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        Vector3 velocityChange = Vector3.zero;
            velocityChange = horInputVec* acceleration.x;
        return velocityChange;
    }

    private Vector3 ProcessVerticalInput()
    {
        Vector3 vertInputVec = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
        Vector3 velocityChange = Vector3.zero;
            velocityChange = vertInputVec* acceleration.y;
        return velocityChange;
    }

}
