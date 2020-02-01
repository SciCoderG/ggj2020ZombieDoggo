﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAttachmentPoint : MonoBehaviour
{
    [SerializeField]
    private Image greenImage;
    [SerializeField]
    private Image redImage;
    /// <summary>
    /// if at least one child is attached, we're assuming an item is attached, so we return true.
    /// </summary>
    public bool HasAttachedItem { get { return null != attachedItem; } }

    private PickupItem attachedItem = null;

    private void Start()
    {
        if(null == attachedItem)
            DisplayDestroyed();
    }

    public void AttachItem(PickupItem item)
    {
        attachedItem = item;
        item.AttachToZombie(this.transform);
        DisplayFullHealth();
    }

    public void Damage(float value)
    {
        if(null != attachedItem)
        {
            greenImage.fillAmount -= value / attachedItem.Life;

            if (greenImage.fillAmount <= 0)
            {
                DisplayDestroyed();
                attachedItem.DestroyItself();
                attachedItem = null;
            }
        }
    }

    private void DisplayDestroyed()
    {
        greenImage.enabled = false;
        redImage.enabled = false;
        greenImage.fillAmount = 0.0f;
    }

    private void DisplayFullHealth()
    {
        redImage.enabled = true;
        greenImage.enabled = true;
        greenImage.fillAmount = 1.0f;
    }
}
