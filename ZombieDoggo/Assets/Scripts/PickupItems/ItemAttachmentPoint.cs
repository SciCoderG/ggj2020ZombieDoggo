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
    private float life = 2.0f;
    /// <summary>
    /// if at least one child is attached, we're assuming an item is attached, so we return true.
    /// </summary>
    public bool HasAttachedItem { get { return transform.childCount > 0; } }

    private void tintLifeBar(float value, bool damage)
    {
        if (damage)
        {
            greenImage.fillAmount -= value/life;

            if (greenImage.fillAmount <= 0)
            {
                greenImage.enabled = false;
                redImage.enabled = false;
            }
        }

        else
        {
            heal();
        }
    }

    private void heal()
    {
        redImage.enabled = true;
        greenImage.enabled = true;
        greenImage.fillAmount = 1;
    }
}
