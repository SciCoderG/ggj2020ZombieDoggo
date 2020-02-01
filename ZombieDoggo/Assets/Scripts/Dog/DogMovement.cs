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
    private float jumpStrength = 2.0f;
    public Vector2 MaxVelocity { get { return maxVelocity; } set { maxVelocity = value; } }
   
    public bool slowDownWhileGrabbing { get; set; }
    private float slowValue = 1f;

    private Rigidbody dogRigidBody = null;
    private CMRotateTowards rotateTowardsScript = null;
    private ForceMode accelerationForceMode = ForceMode.VelocityChange;

    private Animator doggoAnimator = null;

    // Start is called before the first frame update
    void Start()
    {
        doggoAnimator = this.gameObject.GetComponent<Animator>();    
    }

    void Awake()
    {
        dogRigidBody = GetComponent<Rigidbody>();
        rotateTowardsScript = GetComponent<CMRotateTowards>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocityChange = Vector3.zero;
        velocityChange += ProcessHorizontalInput();
        velocityChange += ProcessVerticalInput();

        if (slowDownWhileGrabbing) 
        {
            slowValue = 0.5f;
        }
        else
        {
            slowValue = 1f;
        }


        dogRigidBody.AddForce(velocityChange * Time.fixedDeltaTime, accelerationForceMode);
        dogRigidBody.velocity = Utilities.ClampVector(dogRigidBody.velocity, new Vector3(maxVelocity.x * slowValue, 0.0f, maxVelocity.y * slowValue));

        UpdateRotation();
        doggoAnimator.SetFloat("velocityX", dogRigidBody.velocity.x);
        doggoAnimator.SetFloat("velocityZ", dogRigidBody.velocity.z);
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
        if (Mathf.Abs(dogRigidBody.velocity.x) < maxVelocity.x)
            velocityChange = horInputVec* acceleration.x;
        return velocityChange;
    }

    private Vector3 ProcessVerticalInput()
    {
        Vector3 vertInputVec = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
        Vector3 velocityChange = Vector3.zero;
        if (Mathf.Abs(dogRigidBody.velocity.z) < maxVelocity.y)
            velocityChange = vertInputVec* acceleration.y;
        return velocityChange;
    }

}
