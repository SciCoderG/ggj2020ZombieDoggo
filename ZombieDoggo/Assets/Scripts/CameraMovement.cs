using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraAcceleration = 6.0f;

    public Vector3 Target { get; set; }

    public void LerpToRotation(Quaternion quaternion, float speed = 1.5f)
    {
        StartCoroutine(RotationRoutine(quaternion, speed));
    }

    IEnumerator RotationRoutine(Quaternion targetRot, float speed)
    {
        Quaternion originalRotation = transform.rotation;
        float progress = 0.0f;
        while(progress < 1.0f)
        {
            yield return null;
            progress += Time.deltaTime * speed;
            transform.rotation = Quaternion.Lerp(originalRotation, targetRot, progress);
        }
    }


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
