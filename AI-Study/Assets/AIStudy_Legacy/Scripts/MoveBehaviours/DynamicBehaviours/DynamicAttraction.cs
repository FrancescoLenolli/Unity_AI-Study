using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAttraction : MovementType
{
    public float maxThreshold = 3.0f;
    [Min(0.1f)]
    public float minThreshold = 0.1f;
    public float maxAcceleration = 10.0f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering(true);

        steering.direction = -directions.Separation(target, maxThreshold, maxAcceleration, minThreshold);

        return steering;
    }
}
