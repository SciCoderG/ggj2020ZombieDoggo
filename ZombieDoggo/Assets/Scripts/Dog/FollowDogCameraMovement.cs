using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDogCameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject followObject = null;
    [SerializeField]
    public float zoomValue = 0f;

    private Vector3 initialOffset = Vector3.zero;
   
    public bool isZooming = false;
    private float startFieldOfView;
    private float delta = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        initialOffset = transform.position - followObject.transform.position;
        startFieldOfView = this.GetComponent<Camera>().fieldOfView;
        delta = startFieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, followObject.transform.position.z + initialOffset.z);

       if (isZooming)
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
}
