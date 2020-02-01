using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour
{
    [SerializeField]
    private Transform attachPoint = null;
    public Transform PickupAttachPoint { get { return attachPoint; } }

    private Collider pickupCollider = null;

    private void Awake()
    {
        pickupCollider = GetComponent<Collider>();
    }

    public void Drop(float deactivationTime)
    {
        this.transform.parent = null;
        StartCoroutine(DeactivateColliderForSeconds(deactivationTime));
    }

    private IEnumerator DeactivateColliderForSeconds(float deactivationTime)
    {
        pickupCollider.enabled = false;
        yield return new WaitForSeconds(deactivationTime);
        pickupCollider.enabled = true;

    }
}
