using UnityEngine;

public class KinematicSeek : MovementType
{
    public override Steering GetSteering()
    {
        Steering steering = new Steering(false);

        steering.direction = directions.SeekKinematic(target.position, maxSpeed);
        steering.rotation = rotations.FaceDirection(steering.direction);

        return steering;
    }
}
