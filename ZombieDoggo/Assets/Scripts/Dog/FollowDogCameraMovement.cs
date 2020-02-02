using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraMovement))]
public class FollowDogCameraMovement : MonoBehaviour
{
    [SerializeField]
    public float zoomValue = 0f;
    [SerializeField]
    private float leftRightMaxMovement = 2.0f;

    private Vector3 initialOffset = Vector3.zero;
   
    public bool IsZooming { get; set; }
    private float startFieldOfView;
    private float delta = 0f;
    
    private DogMovement dogObject = null;

    private Vector3 initalPos = Vector3.zero;


    private CameraMovement cameraMovement = null;

    private void Awake()
    {
        if (null == dogObject)
            dogObject = FindObjectOfType<DogMovement>();

        cameraMovement = GetComponent<CameraMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {

        initalPos = transform.position;
        startFieldOfView = this.GetComponent<Camera>().fieldOfView;
        delta = startFieldOfView;
        initialOffset = transform.position - dogObject.transform.position;   
    }

    // Update is called once per frame
    void Update()
    {

       if (IsZooming)
        {
            if (delta > startFieldOfView - zoomValue)
            {
                delta--;
                this.GetComponent<Camera>().fieldOfView = delta;
            }
        }
        else{
            if (delta < startFieldOfView)
            {
                delta++;
                this.GetComponent<Camera>().fieldOfView = delta;
            }
        }
    }

    private void FixedUpdate()
    {
        float distDogCamX = dogObject.transform.position.x - initalPos.x;

        float camXPos = Mathf.Clamp(distDogCamX, -leftRightMaxMovement, leftRightMaxMovement);
        cameraMovement.Target = new Vector3(camXPos, transform.position.y, dogObject.transform.position.z + initialOffset.z);
    }
}
