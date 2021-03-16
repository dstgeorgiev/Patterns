using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetObject;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;

    private void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        
        Vector3 targetedPosition = targetObject.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetedPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
    
}
