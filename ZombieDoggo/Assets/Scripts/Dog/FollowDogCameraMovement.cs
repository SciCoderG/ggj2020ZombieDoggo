using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDogCameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject followObject = null;

    private Vector3 initialOffset = Vector3.zero;
    

    // Start is called before the first frame update
    void Start()
    {
        initialOffset = transform.position - followObject.transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, followObject.transform.position.z + initialOffset.z);
    }
}
