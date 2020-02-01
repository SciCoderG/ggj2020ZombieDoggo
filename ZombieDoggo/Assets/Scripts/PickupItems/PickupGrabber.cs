﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGrabber : MonoBehaviour
{
    [Tooltip("Transform to which the pickup item should be attached.")]
    [SerializeField]
    private Transform grabbingPoint = null;
    [SerializeField]
    private float deactivationTimeAfterDrop = 2.0f;

    private PickupItem currentlyAttachedItem = null;

    private Animator doggoAnimator;

    private void Start()
    {
        doggoAnimator = this.transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Grabbing") && null != currentlyAttachedItem)
        {
            DropCurrentlyAttachedItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PickupItem item = other.GetComponent<PickupItem>();
        if(null != item)
        {
            if (null == currentlyAttachedItem)
                Attach(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickupItem item = other.GetComponent<PickupItem>();
        if (null != item)
        {
            if (item == currentlyAttachedItem)
                currentlyAttachedItem = null;
        }
    }


    public void Attach(PickupItem item)
    {
        if(null == currentlyAttachedItem)
        {
            doggoAnimator.SetTrigger("PickUpTrigger");

            currentlyAttachedItem = item;
            item.transform.SetParent(grabbingPoint);
            item.transform.localPosition = Vector3.zero;
        }
    }

    public void DropCurrentlyAttachedItem()
    {
        doggoAnimator.SetTrigger("DropTrigger");

        currentlyAttachedItem.Drop(deactivationTimeAfterDrop);
        currentlyAttachedItem = null;
    }
}
