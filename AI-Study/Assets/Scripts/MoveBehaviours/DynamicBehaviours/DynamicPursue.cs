using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPursue : DynamicSeek
{
    public float maxPredictionTime = 1.0f;

    private TransformVelocity targetVelocity;

    public override Steering GetSteering()
    {
        Steering steering = new Steering(true);

        Vector3 predictedPosition = directions.PredictPosition(target, targetVelocity, maxPredictionTime);
        steering.direction = directions.SeekDynamic(predictedPosition, maxSpeed);

        return steering;
    }

    public override void InitData()
    {
        targetVelocity = target.GetComponent<TransformVelocity>();
    }
}
