using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDogCameraMovement : MonoBehaviour
{
    [SerializeField]
    public float zoomValue = 0f;

    private Vector3 initialOffset = Vector3.zero;
   
    public bool IsZooming { get; set; }
    private float startFieldOfView;
    private float delta = 0f;
    
    private DogMovement dogObject = null;   



    private void Awake()
    {
        if (null == dogObject)
            dogObject = FindObjectOfType<DogMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
     
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
        this.transform.position = new Vector3(transform.position.x, transform.position.y, dogObject.transform.position.z + initialOffset.z);

    }
}
