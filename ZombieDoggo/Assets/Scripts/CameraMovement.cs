using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraAcceleration = 6.0f;

    public Vector3 Target { get; set; }

    private void Start()
    {
        Target = this.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 cameraMovement = (this.transform.position - Target) * cameraAcceleration;
        
        if(cameraMovement.sqrMagnitude > 0.1f)
        {
            this.transform.position -= cameraMovement * Time.fixedDeltaTime;
        }    
    }
}
