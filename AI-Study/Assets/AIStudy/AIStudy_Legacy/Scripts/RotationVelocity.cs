using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationVelocity : MonoBehaviour
{
    private Vector3 lastRotation;
    private Vector3 velocity;

    public Vector3 Value => velocity;

    private void Awake()
    {
        lastRotation = transform.rotation.eulerAngles;
        velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        CalculateVelocity();
    }

    private void CalculateVelocity()
    {
        if (lastRotation != transform.rotation.eulerAngles)
        {
            velocity = (transform.rotation.eulerAngles - lastRotation) / Time.deltaTime;
            lastRotation = transform.rotation.eulerAngles;
        }
        else
        {
            velocity = Vector3.zero;
        }

        //Utils.ClearLog();
        //Debug.Log("velocity is " + velocity);
    }
}
