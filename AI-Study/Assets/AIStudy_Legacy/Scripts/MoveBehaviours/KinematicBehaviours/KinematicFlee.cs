using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicFlee : MovementType
{
    public override Steering GetSteering()
    {
        Steering steering = new Steering(false);

        steering.direction = directions.FleeKinematic(target.position, maxSpeed);
        steering.rotation = rotations.FaceDirection(steering.direction);

        return steering;
    }
}
