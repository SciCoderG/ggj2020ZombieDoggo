using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour
{
    [SerializeField]
    private Transform attachPoint = null;
    public Transform PickupAttachPoint { get { return attachPoint; } }

}
