using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using de.crystalmesh;

public class LaneSpawner : MonoBehaviour
{
    [SerializeField]
    private Lane[] lanePrefabs = null;
    [SerializeField]
    private int maxAllowedLanes = 5;

    private List<Lane> spawnedLanes = new List<Lane>();

    private void Awake()
    {
        spawnedLanes.Add(FindObjectOfType<Lane>());
        foreach (Lane lane in spawnedLanes)
        {
            lane.OnSpawnNextLane += OnSpawnNewLane;
        }
    }

    public void OnSpawnNewLane(Lane origin)
    {
        Lane prefabTypeToSpawn = Utilities.RandomFromArray(lanePrefabs);

        Lane newLane = Instantiate(prefabTypeToSpawn.gameObject).GetComponent<Lane>();
        newLane.transform.position = origin.NextLaneSpawnPoint.position;
        newLane.OnSpawnNextLane += OnSpawnNewLane;
        newLane.Spawn();
        spawnedLanes.Add(newLane);

        if (spawnedLanes.Count > maxAllowedLanes)
        {
            Lane firstLane = spawnedLanes[0];
            firstLane.Despawn();
            spawnedLanes.Remove(firstLane);
        }
    }
}
