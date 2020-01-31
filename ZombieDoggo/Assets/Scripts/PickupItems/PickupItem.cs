using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour
{
    [SerializeField]
    private string boneTransformName = "Bone";
    [SerializeField]
    private Transform attachPoint = null;
    public string BoneAttachTransformName { get { return boneTransformName; } }
    public Transform PickupAttachPoint { get { return attachPoint; } }

}
