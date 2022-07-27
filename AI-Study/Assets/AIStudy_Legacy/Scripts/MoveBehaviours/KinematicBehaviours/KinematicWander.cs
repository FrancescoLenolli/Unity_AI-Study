using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicWander : MovementType
{
    public float changeRotationTimer = 1.0f;
    public float maxRotationAngle = 90.0f;

    private float currentTimer = 0.0f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering(false);

        steering.direction = directions.MoveForward(maxSpeed);

        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            steering.rotation = rotations.RandomRotation(maxRotationAngle);
            currentTimer = changeRotationTimer;
        }
        else
            steering.rotation = transform.rotation.eulerAngles.z;

        return steering;
    }

    public override void ResetData()
    {
        currentTimer = 0.0f;
    }
}
