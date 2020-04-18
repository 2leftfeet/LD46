using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingAnimation : MonoBehaviour
{
    [SerializeField] float bobbingFrequency = 1.0f;
    [SerializeField] float bobbingMagnitude = 1.0f; 


    [SerializeField] float rotationFrequency = 1.0f;
    [SerializeField] float rotationMagnitude = 1.0f; 

    Vector3 bobbingOffset;
    Vector3 oldBobbingOffset;

    float rotationOffset;
    float rotationOldOffset;

    Rigidbody2D body;
    bool hasReset = false;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(body.velocity.magnitude >= 0.01f)
        {
            hasReset = false;

            bobbingOffset = Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * bobbingFrequency)) * bobbingMagnitude;
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
            }
        }
    }
}
