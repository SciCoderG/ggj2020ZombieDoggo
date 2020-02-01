﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManagementScript : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 25.0f;
    [SerializeField]
    private Transform doggo = null;
    [SerializeField]
    private Camera followDogCamera = null;

    private Animator zombieAnimator = null;
    private bool canGrab = false;
    private bool isDoggoColliding = false;


    // Start is called before the first frame update
    void Start()
    {
        zombieAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Grabbing") && canGrab)
        {
            GrabZombie();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canGrab | transform.parent == null) 
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

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<DogMovement>() != null)
        {
            canGrab = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DogMovement>() != null)
        {
            canGrab = false;
        }
    }

    private void GrabZombie()
    {
        followDogCamera.GetComponent<FollowDogCameraMovement>().IsZooming = true;
        transform.SetParent(doggo);
        StartCoroutine(AnimationCoroutine());
    }

    IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        followDogCamera.GetComponent<FollowDogCameraMovement>().IsZooming = false;
        transform.parent = null;
        canGrab = false;
    }
}

