using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Action<Lane> OnSpawnNextLane;

    [SerializeField]
    private Transform spawnPoint = null;
    public Transform NextLaneSpawnPoint { get { return spawnPoint; } }

    public void SpawnNextLane()
    {
        if (null != OnSpawnNextLane)
            OnSpawnNextLane.Invoke(this);
    }
}
