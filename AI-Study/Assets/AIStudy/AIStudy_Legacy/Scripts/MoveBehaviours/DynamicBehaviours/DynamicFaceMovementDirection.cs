using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFaceMovementDirection : MovementType
{
    public float slowRadius = 2.0f;
    public float targetRadius = 1.0f;
    public float maxRotationAngle = 90.0f;
    public float timeToTarget = 2.0f;
    public float maxAngularAcceleration = 3.0f;

    private TransformVelocity transformVelocity;

    public override Steering GetSteering()
    {
        Steering steering = new Steering(true);

        if (transformVelocity.Value.magnitude == 0)
            return null;

        float orientation = -Mathf.Atan2(-transformVelocity.Value.y, transformVelocity.Value.x) * Mathf.Rad2Deg;
        steering.rotation = rotations.AlignDynamic(orientation, targetRadius, slowRadius, maxRotationAngle, timeToTarget, maxAngularAcceleration);

        return steering;
    }

    public override void InitData()
    {
        transformVelocity = GetComponent<TransformVelocity>();
    }
}
