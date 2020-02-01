using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ZombieManagementScript : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 25.0f;
    [SerializeField]
    private DropItemArea dropArea = null;

    private Animator zombieAnimator = null;

    // Start is called before the first frame update
    void Start()
    {
        zombieAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.parent == null) 
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        CheckIfHitEnvironmental(col);
    }

    private void CheckIfHitEnvironmental(Collider col)
    {
        if (col.GetComponent<EnvironmentalObjects>() != null)
        {
            lifeSpan -= col.gameObject.GetComponent<EnvironmentalObjects>().damageValue;
            if (lifeSpan < 0)
            {
                zombieAnimator.SetTrigger("IsStumbling");
            }
            else
            {
                zombieAnimator.SetTrigger("IsDying");
            }
        }
    }

    public void StartDragging()
    {
        zombieAnimator.SetBool("isDragged", true);
    }

    public void StopDragging()
    {
        zombieAnimator.SetBool("isDragged", false);
        this.transform.forward = Vector3.forward;
        this.transform.parent = null;
    }

}

