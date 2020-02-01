using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using de.crystalmesh;

[RequireComponent(typeof(Collider))]
public class DropItemArea : MonoBehaviour
{
    [SerializeField]
    private Transform ZombieRootNode = null;

    private ItemAttachmentPoint[] attachmentPoints;

    private void Start()
    {
        attachmentPoints = ZombieRootNode.GetComponentsInChildren<ItemAttachmentPoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PickupItem item = other.GetComponent<PickupItem>();
        if(null != item)
        {
            ItemAttachmentPoint attachmentPoint = FindRandomFreeAttachmentPoint();
            if(null != attachmentPoint)
            {
                AttachItemToAttachmentPoint(item, attachmentPoint);
            }
        }
    }

    private static void AttachItemToAttachmentPoint(PickupItem item, ItemAttachmentPoint attachmentPoint)
    {
        Transform bone = attachmentPoint.transform;
        if (null != bone)
        {
            item.transform.SetParent(bone);
            item.transform.localPosition = item.transform.position - item.PickupAttachPoint.position;
            Destroy(item);
        }
    }

    public PickupItem GetRandomAttachedItem()
    {
        PickupItem[] attachedItems = ZombieRootNode.GetComponentsInChildren<PickupItem>();
        return Utilities.RandomFromArray(attachedItems);
    }

    private ItemAttachmentPoint FindRandomFreeAttachmentPoint()
    {
        List<ItemAttachmentPoint> freeAttachmentPoints = new List<ItemAttachmentPoint>();
        foreach(ItemAttachmentPoint attachmentPoint in attachmentPoints)
        {
            if (!attachmentPoint.HasAttachedItem)
                freeAttachmentPoints.Add(attachmentPoint);
        }
         return Utilities.RandomFromList(freeAttachmentPoints);
    }
}
