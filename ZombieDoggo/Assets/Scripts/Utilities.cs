using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace de.crystalmesh
{
    public static class Utilities
    {
        public static Vector3 ClampVector(Vector3 inputVelocity, Vector3 maxAbsVelocity)
        {
            Vector3 clamped = inputVelocity;
            clamped.x = Mathf.Clamp(inputVelocity.x, -maxAbsVelocity.x, maxAbsVelocity.x);
            clamped.y = Mathf.Clamp(inputVelocity.y, -maxAbsVelocity.y, maxAbsVelocity.y);
            clamped.z = Mathf.Clamp(inputVelocity.z, -maxAbsVelocity.z, maxAbsVelocity.z);
            return clamped;
        }

        public static T RandomFromList<T>(List<T> list)
        {
            T randomItem = default(T);
            if(list.Count > 0)
            {
                int randomEntry = Random.Range(0, list.Count - 1);
                randomItem = list[randomEntry];
            }
            return randomItem;
        }
    }
}

