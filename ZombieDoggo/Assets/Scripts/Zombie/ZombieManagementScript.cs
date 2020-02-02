using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class ZombieManagementScript : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 25.0f;
    [SerializeField]
    private float zombieSpeed = 1.0f;


    [SerializeField]
    private float increaseMultiplier = 0.01f;
    [SerializeField]
    private float maxSpeed = 4.0f;

    public float ZombieSpeed { get { return zombieSpeed; } set { zombieSpeed = value; } }

    [SerializeField]
    private DropItemArea dropArea = null;

    [SerializeField]
    private Animator zombieAnimator = null;
    public Animator ZombieAnimator { get { return zombieAnimator; } }

    private Rigidbody zombieRB = null;

    private bool isDragged = false;

    private Vector3 movementDirection = Vector3.forward;


    private Vector3 originalPosition = Vector3.zero;
    private float originalSpeed = 0.0f;

    private float speedMultiplieder = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = zombieSpeed;
        originalPosition = transform.position;
        zombieRB = GetComponent<Rigidbody>();
    }

    public void StopZombie()
    {
        zombieRB.velocity = Vector3.zero;
        speedMultiplieder = 0.0f;
    }

    public void PlayDeathAnimation()
    {
        zombieAnimator.SetTrigger("IsDying");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.forward = Vector3.forward;

        Vector3 newPlanarVelocity = movementDirection * zombieSpeed;
        float clampedVerticalSpeed = Mathf.Clamp(zombieRB.velocity.y, -5.0f, 5.0f);
        zombieRB.velocity = new Vector3(newPlanarVelocity.x, clampedVerticalSpeed, newPlanarVelocity.z) * speedMultiplieder;
        zombieAnimator.SetFloat("MovementStateMachine", zombieRB.velocity.magnitude);
        IncreaseSpeed();
    }

    private void IncreaseSpeed()
    {
        float distanceToOrigin = Mathf.Abs(transform.position.z - originalPosition.z);
        zombieSpeed = distanceToOrigin * increaseMultiplier + originalSpeed;
        zombieSpeed = Mathf.Clamp(zombieSpeed, 0, maxSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.GetContact(0).normal;
        if (Mathf.Abs(normal.z) > 0.5f && Mathf.Abs(normal.y) < 0.2f)
        {
            movementDirection = Vector3.right * Mathf.Sign(normal.x) + Vector3.back * 0.1f;
        }
        else
        {
            movementDirection = Vector3.forward;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(ReturnToWalkingForward());
    }

    private IEnumerator ReturnToWalkingForward()
    {
        yield return new WaitForSeconds(0.1f);
        movementDirection = Vector3.forward;
    }

    void OnTriggerEnter(Collider col)
    {
        CheckIfHitEnvironmental(col);
    }
    private void CheckIfHitEnvironmental(Collider col)
    {
        if (col.GetComponent<EnvironmentalObjects>() != null)
        {
            lifeSpan -= col.gameObject.GetComponent<EnvironmentalObjects>().damageValue;
            if (lifeSpan < 0)
            {
                zombieAnimator.SetTrigger("IsStumbling");
            }
            else
            {
                zombieAnimator.SetTrigger("IsDying");
            }
        }
    }

    public void StartDragging()
    {
        zombieAnimator.SetBool("isDragged", true);
        StopZombie();
    }

    public void StopDragging()
    {
        zombieAnimator.SetBool("isDragged", false);
        this.transform.forward = Vector3.forward;
        this.transform.parent = null;
        speedMultiplieder = 1.0f;
    }


}

