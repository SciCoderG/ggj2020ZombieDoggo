using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttachmentPoint : MonoBehaviour
{
    /// <summary>
    /// if at least one child is attached, we're assuming an item is attached, so we return true.
    /// </summary>
    public bool HasAttachedItem { get { return transform.childCount > 0; } }
}
