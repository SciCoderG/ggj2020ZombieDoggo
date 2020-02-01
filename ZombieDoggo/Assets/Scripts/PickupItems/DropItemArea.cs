using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DropItemArea : MonoBehaviour
{
    [SerializeField]
    private Transform ZombieRootNode = null;
    private void OnTriggerEnter(Collider other)
    {
        PickupItem item = other.GetComponent<PickupItem>();
        if(null != item)
        {
            Transform bone = ZombieRootNode.Find(item.BoneAttachTransformName);
            if(null != bone)
            {
                item.transform.SetParent(bone);
                item.transform.localPosition = item.transform.position - item.PickupAttachPoint.position;
                Destroy(item);
            }
        }
    }
}
