using UnityEngine;

public class DynamicArrive : MovementType
{
    public float maxAcceleration = 2.0f;
    public float targetSpeed = 2.0f;
    public float targetRadius = 0.5f;
    public float slowRadius = 3.0f;
    public float timeToTarget = 0.1f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering(true);

        steering.direction = directions.ArriveDynamic(target.position, targetRadius, slowRadius, targetSpeed, maxSpeed, timeToTarget, maxAcceleration);

        return steering;
    }
}
