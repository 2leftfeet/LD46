using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingAnimation : MonoBehaviour
{
    [SerializeField] float bobbingFrequency = 1.0f;
    [SerializeField] float bobbingMagnitude = 1.0f; 


    [SerializeField] float rotationFrequency = 1.0f;
    [SerializeField] float rotationMagnitude = 1.0f; 

    float bobbingFrequencyOffset;
    Vector3 bobbingOffset;
    Vector3 oldBobbingOffset;

    float rotationOffset;
    float rotationOldOffset;

    Rigidbody2D body;
    bool hasReset = false;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        if(!body)
        {
            body = GetComponentInParent<Rigidbody2D>();
        }
        bobbingFrequencyOffset = Random.Range(0, 2*Mathf.PI);
    }

    void FixedUpdate()
    {
        if(body.velocity.magnitude >= 0.1f)
        {
            hasReset = false;

            bobbingOffset = Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * bobbingFrequency + bobbingFrequencyOffset)) * bobbingMagnitude;
            transform.position += bobbingOffset - oldBobbingOffset; 
            oldBobbingOffset = bobbingOffset;

            rotationOffset = Mathf.Sin(Time.time * rotationFrequency) * rotationMagnitude;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + rotationOffset - rotationOldOffset);
            rotationOldOffset = rotationOffset;
        }
        else
        {
            if(!hasReset)
            {
                transform.position -= oldBobbingOffset;
                transform.rotation = Quaternion.identity;
                hasReset = true;
                oldBobbingOffset = Vector3.zero;
            }
        }
    }
}
