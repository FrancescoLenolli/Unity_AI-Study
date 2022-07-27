using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicWander : MovementType
{
    public float wanderOffset = 1.0f;
    public float wanderRadius = 3.0f;
    public float wanderRate = 1.0f;
    public float wanderOrientation = 2.0f;
    public float maxAcceleration = 2.0f;
    public float slowRadius = 2.0f;
    public float targetRadius = 1.0f;
    public float maxRotationAngle = 90.0f;
    public float timeToTarget = 2.0f;
    public float maxAngularAcceleration = 3.0f;
    public float rotationTimer = 2.0f;

    private Vector3 randomDirection = Vector3.zero;
    private float currentTimer = 0.0f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering(true);

        currentTimer -= Time.deltaTime;
        if(currentTimer <= 0.0f)
        {
            randomDirection = directions.RandomDirectionSimple();
            currentTimer = rotationTimer;
        }
        steering.rotation = rotations.FaceDynamic(randomDirection, targetRadius, slowRadius, maxRotationAngle, timeToTarget, maxAngularAcceleration);
        steering.direction = maxAcceleration * transform.right;

        return steering;
    }

    public override void ResetData()
    {
        currentTimer = 0.0f;
        randomDirection = Vector3.zero;
    }
}
