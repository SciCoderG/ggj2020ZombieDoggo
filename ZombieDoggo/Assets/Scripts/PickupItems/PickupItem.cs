using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour
{
    [SerializeField]
    private float life = 3.0f;
    public float Life { get { return life; } }

    [SerializeField]
    private Transform root = null;
    [SerializeField]
    private Transform attachPoint = null;
    [SerializeField]
    private float dropForce = 10.0f;

    public Transform PickupAttachPoint { get { return attachPoint; } }
    public bool IsCarried { get; set; } = false;

    private Collider pickupCollider = null;
    private Rigidbody itemRB = null;

    private void Awake()
    {
        pickupCollider = GetComponent<Collider>();
        itemRB = root.GetComponent<Rigidbody>();
    }

    public void Pickup(Transform grabbingPoint)
    {
        IsCarried = true;
        root.parent = grabbingPoint;
        root.localPosition = Vector3.zero;
        root.forward = grabbingPoint.forward;
        itemRB.isKinematic = true;
        pickupCollider.isTrigger = true;
        pickupCollider.enabled = true;

    }

    public void AttachToZombie(Transform bone)
    {
        root.parent = bone;
        IsCarried = false;
        root.forward = bone.forward;
        float localZPosition = -PickupAttachPoint.localPosition.z * root.localScale.z;
        root.localPosition = new Vector3(0.0f, 0.0f, localZPosition);
        //root.localPosition = -PickupAttachPoint.localPosition * root.localScale.z;
        itemRB.isKinematic = true;
        pickupCollider.enabled = false;
    }

    public void Drop()
    {
        root.SetParent(null);
        IsCarried = false;
        itemRB.isKinematic = false;
        pickupCollider.enabled = true;
        pickupCollider.isTrigger = false;

        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = 1.0f;

        itemRB.AddForce(dropForce * randomDirection, ForceMode.VelocityChange);
    }

    public void DestroyItself()
    {
        Drop();
        pickupCollider.enabled = false;
        Destroy(root.gameObject, 3.0f);
    }
}
