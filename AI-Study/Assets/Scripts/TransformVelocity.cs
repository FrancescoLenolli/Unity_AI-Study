using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformVelocity : MonoBehaviour
{
    private Vector3 lastPosition;
    private Vector3 velocity;

    public Vector3 Value => velocity;

    private void Awake()
    {
        lastPosition = transform.position;
        velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        CalculateVelocity();
    }

    private void CalculateVelocity()
    {
        if (lastPosition != transform.position)
        {
            velocity = (transform.position - lastPosition) / Time.deltaTime;
            lastPosition = transform.position;
        }
        else
        {
            velocity = Vector3.zero;
        }

        //Utils.ClearLog();
        //Debug.Log("velocity is " + velocity);
    }
}
