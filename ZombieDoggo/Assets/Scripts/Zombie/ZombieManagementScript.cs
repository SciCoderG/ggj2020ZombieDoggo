using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManagementScript : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 25.0f;
    [SerializeField]
    private GameObject doggo = null;
    [SerializeField]
    private Camera followDogCamera = null;

    private Animator zombieAnimator = null;
    private bool isGrabbing = false;
    private bool isDoggoColliding = false;


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
                followDogCamera.GetComponent<FollowDogCameraMovement>().isZooming = true;
                transform.SetParent(doggo.transform);
                doggo.GetComponent<DogMovement>().slowDownWhileGrabbing = true;
                StartCoroutine(AnimationCoroutine());
            }
        }
    }

    IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        followDogCamera.GetComponent<FollowDogCameraMovement>().isZooming = false;
        doggo.GetComponent<DogMovement>().slowDownWhileGrabbing = false;
        transform.parent = null;
        isGrabbing = false;
    }
}

