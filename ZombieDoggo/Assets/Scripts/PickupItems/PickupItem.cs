using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        PickupGrabber grabber = other.GetComponent<PickupGrabber>();
        if(null != grabber)
        {
            grabber.StartTouch(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickupGrabber grabber = other.GetComponent<PickupGrabber>();
        if (null != grabber)
        {
            grabber.StopTouch(this);
        }
    }
}
