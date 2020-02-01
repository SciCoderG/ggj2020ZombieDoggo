using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour
{
    [SerializeField]
    private Transform root = null;
    [SerializeField]
    private Transform attachPoint = null;
    public Transform PickupAttachPoint { get { return attachPoint; } }
    public bool IsCarried { get; set; } = false;

    private Collider pickupCollider = null;

    private void Awake()
    {
        pickupCollider = GetComponent<Collider>();
    }

    public void Pickup(Transform grabbingPoint)
    {
        IsCarried = true;
        root.parent = grabbingPoint;
        root.localPosition = Vector3.zero;
        root.forward = grabbingPoint.forward;
    }

    public void AttachToZombie(Transform bone)
    {
        root.SetParent(bone);
        root.forward = bone.forward;
        root.localPosition = -PickupAttachPoint.localPosition;
    }

    public void Drop(float deactivationTime)
    {
        this.transform.parent = null;
        IsCarried = false;
        StartCoroutine(DeactivateColliderForSeconds(deactivationTime));
    }

    private IEnumerator DeactivateColliderForSeconds(float deactivationTime)
    {
        pickupCollider.enabled = false;
        yield return new WaitForSeconds(deactivationTime);
        pickupCollider.enabled = true;

    }
}
