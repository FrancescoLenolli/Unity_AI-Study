using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFlee : MovementType
{
    public override Steering GetSteering()
    {
        Steering steering = new Steering(true);

        steering.direction = directions.FleeDynamic(target, maxSpeed);

        return steering;
    }
}
