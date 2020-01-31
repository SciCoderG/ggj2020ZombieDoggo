using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGrabber : MonoBehaviour
{
    [Tooltip("Transform to which the pickup item should be attached.")]
    [SerializeField]
    private Transform grabbingPoint = null;

    private PickupItem currentlyAttachedItem = null;

    private List<PickupItem> touchedItems = new List<PickupItem>();
    public void StartTouch(PickupItem item)
    {
        touchedItems.Add(item);
    }

    public void StopTouch(PickupItem item)
    {
        touchedItems.Remove(item);
    }

    private void Update()
    {
        if (touchedItems.Count > 0)
        {
            Attach(touchedItems[0]);
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
        item.transform.SetParent(null);
        currentlyAttachedItem = null;
    }
}
