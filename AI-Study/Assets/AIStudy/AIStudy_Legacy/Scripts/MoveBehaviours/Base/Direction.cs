using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction
{
    private static Vector3 Invalid => Vector3.negativeInfinity;

    private Transform owner;
    private TransformVelocity transformVelocity;

    public Direction(Transform owner)
    {
        this.owner = owner;
        transformVelocity = owner.GetComponent<TransformVelocity>();
    }

    public static bool IsValid(Vector3 direction)
    {
        return !float.IsInfinity(direction.magnitude);
    }

    public Vector3 RandomDirectionComplex(float wanderRate, float wanderOffset, float wanderRadius)
    {
        float wanderOrientation = Utils.RandomBinomial() * wanderRate;

        float targetOrientation = wanderOrientation + owner.rotation.eulerAngles.z;
        Vector3 target = owner.position + wanderOffset * owner.right;
        target += wanderRadius * Utils.RightDirection2D(new Vector3(0, 0, targetOrientation));

        return target;
    }

    public Vector3 RandomDirectionSimple()
    {
        Vector3 randomVector = new Vector3(Utils.RandomBinomial(), Utils.RandomBinomial(), 0.0f);
        Vector3 result = randomVector + owner.position;
        return result;
    }

    public Vector3 FleeKinematic(Vector3 target, float maxSpeed)
    {
        Vector3 direction = maxSpeed * -GetMovementDirection(target);
        return direction;
    }

    public Vector3 SeekKinematic(Vector3 target, float maxSpeed)
    {
        Vector3 direction = maxSpeed * GetMovementDirection(target); 
        return direction;
    }

    public Vector3 MoveForward(float maxSpeed)
    {
        Vector3 direction = maxSpeed * owner.right;
        return direction;
    }

    public Vector3 ArriveDynamic(Vector3 target, float targetRadius, float slowRadius, float targetSpeed, float maxSpeed, float timeToTarget, float maxAcceleration)
    {
        Vector3 resultDirection;
        Vector3 targetVelocity;

        Vector3 direction = GetMovementDirection(target);
        float distance = direction.magnitude;
        if (distance < targetRadius)
            return Invalid;
        if (distance > slowRadius)
            targetSpeed = maxSpeed;
        else
            targetSpeed = maxSpeed * distance / slowRadius;

        targetVelocity = direction;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;
        resultDirection = targetVelocity - transformVelocity.Value;
        resultDirection /= timeToTarget;

        if (resultDirection.magnitude > maxAcceleration)
        {
            resultDirection.Normalize();
            resultDirection *= maxAcceleration;
        }

        return resultDirection;
    }

    public Vector3 FleeDynamic(Transform target, float maxSpeed)
    {
        Vector3 direction = -GetMovementDirection(target);
        direction.Normalize();
        direction *= maxSpeed;

        return direction;
    }

    public Vector3 SeekDynamic(Transform target, float maxSpeed)
    {
        Vector3 direction = GetMovementDirection(target);
        direction.Normalize();
        direction *= maxSpeed;

        return direction;
    }

    public Vector3 SeekDynamic(Vector3 target, float maxSpeed)
    {
        Vector3 direction = GetMovementDirection(target);
        direction.Normalize();
        direction *= maxSpeed;

        return direction;
    }

    public Vector3 PredictPosition(Transform target, TransformVelocity targetVelocity, float maxPredictionTime)
    {
        Vector3 targetPosition = target.position;
        Vector3 targetDirection = GetMovementDirection(target);
        float distance = targetPosition.magnitude;
        float speed = transformVelocity.Value.magnitude;
        float prediction;

        if (speed <= distance / maxPredictionTime)
            prediction = maxPredictionTime;
        else
            prediction = distance / speed;

        Vector3 newTargetPosition = targetPosition;
        newTargetPosition += targetVelocity.Value * prediction;

        return newTargetPosition;
    }

    public Vector3 VelocityMatch(TransformVelocity targetVelocity, float timeToTarget, float maxAcceleration)
    {
        Vector3 direction = targetVelocity.Value - transformVelocity.Value;
        direction /= timeToTarget;

        if (direction.magnitude > maxAcceleration)
        {
            direction.Normalize();
            direction *= maxAcceleration;
        }

        return direction;
    }

    // Strange behaviour when used in DynamicAttraction
    public Vector3 Separation(Transform target, float threshold, float maxAcceleration, float minThreshold = -1.0f)
    {
        Vector3 result = Vector3.zero;

        Vector3 direction = GetMovementDirection(target);
        float distance = direction.magnitude;
        float strength = 0.0f;

        if (distance > threshold || minThreshold > 0 && distance <= minThreshold)
        {
            result = Invalid;
            return result;
        }

        strength = LinearSeparation(maxAcceleration, threshold, distance);
        direction.Normalize();
        result += -strength * direction;

        return result;
    }

    private Vector3 GetMovementDirection(Transform target)
    {
        return target.position - owner.position;
    }

    private Vector3 GetMovementDirection(Vector3 targetPosition)
    {
        return targetPosition - owner.position;
    }

    private float LinearSeparation(float maxAcceleration, float threshold, float distance)
    {
        float strength = maxAcceleration * (threshold - distance) / threshold;
        return strength;
    }

    private float InverseSquareLawSeparation(float decayCoefficient, float distance, float maxAcceleration)
    {
        float strength = Mathf.Min(decayCoefficient / (distance * distance), maxAcceleration);
        return strength;
    }
}
