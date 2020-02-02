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
        StartCoroutine(AnimateSpawn(0, 1));
    }

    public void Despawn()
    {
        StartCoroutine(AnimateSpawn(1, 0));
        Destroy(this.gameObject, 1.0f);
    }

    private IEnumerator AnimateSpawn(float from, float to)
    {
        float delta = to - from;

        float interpolation = 0.0f;
        while (Mathf.Abs(interpolation) < Mathf.Abs(delta))
        {
            transform.position = new Vector3(
            transform.position.x,
            spawnCurve.Evaluate(from + interpolation),
            transform.position.z);
            yield return null;
            interpolation += Mathf.Sign(delta) * Time.deltaTime;
        }
    }
}
