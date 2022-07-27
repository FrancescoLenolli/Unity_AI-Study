using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicVelocityMatch : MovementType
{
    public float maxAcceleration = 2.0f;
    public float timeToTarget = 0.1f;

    private TransformVelocity targetVelocity;

    public override Steering GetSteering()
    {
        Steering steering = new Steering(true);

        steering.direction = directions.VelocityMatch(targetVelocity, timeToTarget, maxAcceleration);

        return steering;
    }

    public override void InitData()
    {
        targetVelocity = target.GetComponent<TransformVelocity>();
    }  
}
