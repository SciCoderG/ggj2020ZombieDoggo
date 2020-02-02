using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ZombieGrabber : MonoBehaviour
{
    [SerializeField]
    private float grabZOffset = 5f;
    [SerializeField]
    private float draggingTime = 0.75f;
    [Header("External References")]
    [SerializeField]
    private FollowDogCameraMovement followDogCamera = null;
    [SerializeField]
    private ZombieManagementScript zombieMovement = null;
    [Header("Internal References")]
    [SerializeField]
    private DogMovement dogMovement = null;
    [SerializeField]
    private Animator dogAnimator = null;


    private bool CanGrab = false;

    private void Awake()
    {
        if (null == followDogCamera)
            followDogCamera = FindObjectOfType<FollowDogCameraMovement>();
        if (null == dogMovement)
            dogMovement = FindObjectOfType<DogMovement>();
        if (null == zombieMovement)
            zombieMovement = FindObjectOfType<ZombieManagementScript>();
        if (null == dogAnimator)
            dogAnimator = dogMovement.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Grabbing")&& CanGrab)
        {
            GrabZombie();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GrabbableByDoggoArea zombie = other.GetComponent<GrabbableByDoggoArea>();
        if (null != zombie)
            CanGrab = true;
    }

    private void OnTriggerExit(Collider other)
    {
        GrabbableByDoggoArea zombie = other.GetComponent<GrabbableByDoggoArea>();
        if (null != zombie)
            CanGrab = false;
    }

    private void GrabZombie()
    {
        followDogCamera.IsZooming = true;
        zombieMovement.transform.parent = this.transform;
        zombieMovement.transform.localPosition = new Vector3(0, zombieMovement.transform.localPosition.y, grabZOffset);
        zombieMovement.StartDragging();
        dogMovement.SlowDownWhileDragging = true;

        dogAnimator.SetBool("isDragging", true);

        //Sound grabZombie
        this.GetComponents<AudioSource>()[0].Play();

        StartCoroutine(AnimationCoroutine());
    }

    IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(draggingTime);
        dogMovement.SlowDownWhileDragging = false;
        followDogCamera.IsZooming = false;
        zombieMovement.StopDragging();
        dogAnimator.SetBool("isDragging", false);

        //Sound letZombieGo
        this.GetComponents<AudioSource>()[1].Play();
    }
}
