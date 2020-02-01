using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OnCollisionGetDamage : MonoBehaviour
{
    [SerializeField]
    DropItemArea dropItemArea = null;
    [SerializeField]
    private float damageThreshold = 0.2f;
    [SerializeField]
    private float timeBetweenDamage = 1.0f;

    private ItemAttachmentPoint currentlyDamagedBone = null;

    private bool isDamageable = true;


    private void Start()
    {
        PickupItem[] startItems = GetComponentsInChildren<PickupItem>();
        foreach (PickupItem item in startItems)
            dropItemArea.AttachItem(item);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.GetContact(0).normal;
        Vector3 projected = Vector3.ProjectOnPlane(normal, Vector3.up);
        if(isDamageable && projected.magnitude > damageThreshold)
        {
            Damage();
        }
    }

    private void Damage()
    {
        if(null == currentlyDamagedBone || !currentlyDamagedBone.HasAttachedItem)
            currentlyDamagedBone = dropItemArea.GetRandomUsedAttachmentPoint();
        if(null != currentlyDamagedBone)
        {
            currentlyDamagedBone.Damage(1.0f);
            StartCoroutine(WaitBeforeDamagePossibleAgain());
        }
        else
        {
            OnZombieDied();
        }
        
    }

    private void OnZombieDied()
    {
        // TODO
    }

    private IEnumerator WaitBeforeDamagePossibleAgain()
    {
        isDamageable = false;
        yield return new WaitForSeconds(timeBetweenDamage);
        isDamageable = true;
    }
}
