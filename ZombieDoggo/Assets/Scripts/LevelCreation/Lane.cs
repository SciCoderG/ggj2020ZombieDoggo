using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Action<Lane> OnSpawnNextLane;

    [SerializeField]
    private Transform spawnPoint = null;
    [SerializeField]
    private AnimationCurve spawnCurve = null;
    public Transform NextLaneSpawnPoint { get { return spawnPoint; } }

    public void SpawnNextLane()
    {
        if (null != OnSpawnNextLane)
            OnSpawnNextLane.Invoke(this);
    }

    public void Spawn()
    {
        StartCoroutine(AnimateSpawn());
    }

    private IEnumerator AnimateSpawn()
    {
        float deltaTime = 0.0f;
        while(deltaTime < 1.0f)
        {
            transform.position = new Vector3(
            transform.position.x,
            spawnCurve.Evaluate(deltaTime),
            transform.position.z);
            yield return null;
            deltaTime += Time.deltaTime;
        }
    }
}
