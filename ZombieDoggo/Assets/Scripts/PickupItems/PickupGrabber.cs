using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGrabber : MonoBehaviour
{
    [Tooltip("Transform to which the pickup item should be attached.")]
    [SerializeField]
    private Transform grabbingPoint = null;
    [SerializeField]
    private float deactivationTimeAfterDrop = 2.0f;
    [SerializeField]
    private float waitTimeBeforeThrowItem = 0.5f;

    private PickupItem currentlyAttachedItem = null;

    [SerializeField]
    private Animator doggoAnimator;

    private bool canPickup = true;

    private void Update()
    {
        if(null != currentlyAttachedItem && !currentlyAttachedItem.IsCarried)
        {
            currentlyAttachedItem = null;
        }
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
        if(canPickup && null == currentlyAttachedItem)
        {
            doggoAnimator.SetTrigger("PickUpTrigger");

            currentlyAttachedItem = item;

            item.Pickup(grabbingPoint);
        }


        //Sound pickUp
        this.GetComponents<AudioSource>()[0].Play();
    }

    public void DropCurrentlyAttachedItem()
    {
        StartCoroutine(WaitBeforeNextPickup());
        doggoAnimator.SetTrigger("DropTrigger");

        StartCoroutine(WaitBeforeStartDrop());

        //Sound drop
        this.GetComponents<AudioSource>()[1].Play();
    }

    private IEnumerator WaitBeforeStartDrop()
    {
        yield return new WaitForSeconds(waitTimeBeforeThrowItem);
        currentlyAttachedItem.Drop();
        currentlyAttachedItem = null;
    }

    private IEnumerator WaitBeforeNextPickup()
    {
        canPickup = false;
        yield return new WaitForSeconds(deactivationTimeAfterDrop);
        canPickup = true;
    }
}
