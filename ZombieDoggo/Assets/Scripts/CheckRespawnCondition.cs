using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRespawnCondition : MonoBehaviour
{
    [SerializeField]
    private float yCoordinateForReset = -50.0f;
    [SerializeField]
    private DogMovement dogReference = null;
    [SerializeField]
    private ZombieManagementScript zombieReference = null;

    private void FixedUpdate()
    {
        bool isDogDead = dogReference.transform.position.y < yCoordinateForReset;
        bool isZombieDead = zombieReference.transform.position.y < yCoordinateForReset;
        if (isDogDead || isZombieDead)
        {
            ZombieDeathBehavior deathBehavior = zombieReference.GetComponentInChildren<ZombieDeathBehavior>();
            deathBehavior.OnDeath();
        }
    }
}
