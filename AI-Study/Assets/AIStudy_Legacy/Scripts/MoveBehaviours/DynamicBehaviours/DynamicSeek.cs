using UnityEngine;

public class DynamicSeek : MovementType
{
    public override Steering GetSteering()
    {
        Steering steering = new Steering(true);

        steering.direction = directions.SeekDynamic(target, maxSpeed);

        return steering;
    }
}
