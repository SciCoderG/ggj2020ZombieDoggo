using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NextLaneSpawnTrigger : MonoBehaviour
{
    [SerializeField]
    private Lane ConnectedLane = null;
    private void OnTriggerEnter(Collider other)
    {
        if(null != other.GetComponent<ZombieManagementScript>())
        {
            ConnectedLane.SpawnNextLane();
        }

    }
}
