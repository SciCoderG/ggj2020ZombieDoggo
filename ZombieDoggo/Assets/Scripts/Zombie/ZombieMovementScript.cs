using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovementScript : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 25.0f;
    [SerializeField]
    private Transform doggo = null;

    private Animator zombieAnimator = null;
    private bool isGrabbing = false;

    // Start is called before the first frame update
    void Start()
    {
        zombieAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Grabbing"))
        {
            isGrabbing = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isGrabbing | transform.parent == null) 
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<EnvironmentalObjects>() != null)
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

        if(col.GetComponent<DogMovement>() != null)
        {
            if (isGrabbing)
            {
                transform.SetParent(doggo);
                StartCoroutine(AnimationCoroutine());
            }
        }
    }

    IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(1f);
        transform.parent = null;
        isGrabbing = false;
    }
}

