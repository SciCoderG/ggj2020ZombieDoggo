using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class ZombieManagementScript : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 25.0f;
    [SerializeField]
    private float zombieSpeed = 1.0f;
    [SerializeField]
    private DropItemArea dropArea = null;

    private Animator zombieAnimator = null;
    private Rigidbody zombieRB = null;

    private Vector3 movementDirection = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        zombieAnimator = GetComponent<Animator>();
        zombieRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.parent == null) 
        {
            zombieRB.velocity = movementDirection * zombieSpeed;
            //transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.GetContact(0).normal;
        if (Mathf.Abs(normal.x) > 0)
        {
            movementDirection = Vector3.right * Mathf.Sign(normal.x) + Vector3.back * 0.1f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(ReturnToWalkingForward());
    }

    private IEnumerator ReturnToWalkingForward()
    {
        yield return new WaitForSeconds(0.5f);
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
    }

    public void StopDragging()
    {
        zombieAnimator.SetBool("isDragged", false);
        this.transform.forward = Vector3.forward;
        this.transform.parent = null;
    }

}

