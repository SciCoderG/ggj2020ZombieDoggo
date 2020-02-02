using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAttachmentPoint : MonoBehaviour
{
    [SerializeField]
    private Image greenImage;
    [SerializeField]
    private Image redImage;
    [SerializeField]
    private AudioSource zombieAudioSource = null;
    [SerializeField]
    private AudioClip clip = null;
    /// <summary>
    /// if at least one child is attached, we're assuming an item is attached, so we return true.
    /// </summary>
    public bool HasAttachedItem { get { return null != attachedItem; } }

    private PickupItem attachedItem = null;

    private void Awake()
    {
        attachedItem = GetComponentInChildren<PickupItem>();
    }

    private void Start()
    {
        if(null == attachedItem)
            DisplayDestroyed();
        else
        {
            AttachItem(attachedItem);
        }
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
                zombieAudioSource.PlayOneShot(clip, 3.0f);
                attachedItem.DestroyItself();
                attachedItem = null;
            }
        }
    }

    private void DisplayDestroyed()
    {
        if(null != greenImage && null != redImage)
        {
            greenImage.enabled = false;
            redImage.enabled = false;
            greenImage.fillAmount = 0.0f;
        }
        
    }

    private void DisplayFullHealth()
    {
        if(null != greenImage && null != redImage)
        {
            redImage.enabled = true;
            greenImage.enabled = true;
            greenImage.fillAmount = 1.0f;
        }
    }
}
