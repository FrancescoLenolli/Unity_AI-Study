using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAlign : MovementType
{
    public float slowRadius = 2.0f;
    public float targetRadius = 1.0f;
    public float maxRotationAngle = 90.0f;
    public float timeToTarget = 2.0f;
    public float maxAngularAcceleration = 3.0f;
    public override Steering GetSteering()
    {
        Steering steering = new Steering(true);

        float targetRotationAngle = target.rotation.eulerAngles.z;
        steering.rotation = rotations.AlignDynamic(targetRotationAngle, targetRadius, slowRadius, maxRotationAngle, timeToTarget, maxAngularAcceleration);

        return steering;
    }
}
