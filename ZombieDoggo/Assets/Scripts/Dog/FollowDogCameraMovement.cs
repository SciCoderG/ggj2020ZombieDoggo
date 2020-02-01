using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDogCameraMovement : MonoBehaviour
{
    [SerializeField]
    private DogMovement dogObject = null;   

    private Vector3 initialOffset = Vector3.zero;


    private void Awake()
    {
        if (null == dogObject)
            dogObject = FindObjectOfType<DogMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        initialOffset = transform.position - dogObject.transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, dogObject.transform.position.z + initialOffset.z);
    }
}
