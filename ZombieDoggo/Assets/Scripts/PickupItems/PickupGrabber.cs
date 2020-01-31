using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGrabber : MonoBehaviour
{
    [Tooltip("Transform to which the pickup item should be attached.")]
    [SerializeField]
    private Transform grabbingPoint = null;

    private PickupItem currentlyAttachedItem = null;


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
                Dettach(item);
        }
    }


    public void Attach(PickupItem item)
    {
        if(null == currentlyAttachedItem)
        {
            currentlyAttachedItem = item;
            item.transform.SetParent(grabbingPoint);
            item.transform.localPosition = Vector3.zero;
        }
    }

    public void Dettach(PickupItem item)
    {
        currentlyAttachedItem = null;
    }
}
