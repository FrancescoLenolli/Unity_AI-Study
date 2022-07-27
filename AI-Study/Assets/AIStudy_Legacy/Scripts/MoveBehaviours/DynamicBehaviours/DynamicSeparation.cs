using UnityEngine;

public class DynamicSeparation : MovementType
{
    // max distance where separation start to take effect
    public float threshold = 1.0f;
    // how much the separation force decay the more the object is far from the target
    public float decayCoefficient = 1.0f;
    public float maxAcceleration = 5.0f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering(true);

        steering.direction = directions.Separation(target, threshold, maxAcceleration);

        return steering;
    }
}
