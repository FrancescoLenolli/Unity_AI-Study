using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation
{
    private Transform owner;

    public Rotation(Transform owner)
    {
        this.owner = owner;
    }

    public float RandomRotation(float maxRotationAngle)
    {
        float rotation = Utils.RandomBinomial() * maxRotationAngle;
        return rotation;
    }

    public float FaceDirection(Vector3 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    public float AlignDynamic(float targetRotationAngle, float targetRadius, float slowRadius, float maxRotationAngle, float timeToTarget, float maxAngularAcceleration)
    {
        float targetRotation;
        float rotation = Mathf.DeltaAngle(owner.rotation.eulerAngles.z, targetRotationAngle);
        float rotationSize = Mathf.Abs(rotation);
        float steeringRotation;

        if (rotationSize < targetRadius)
            return 0;

        if (rotationSize > slowRadius)
            targetRotation = maxRotationAngle;
        else
            targetRotation = maxRotationAngle * rotationSize / slowRadius;

        targetRotation *= rotation / rotationSize;

        steeringRotation = targetRotation - owner.rotation.eulerAngles.z;
        steeringRotation /= timeToTarget;

        float angularAcceleration = Mathf.Abs(steeringRotation);
        if (angularAcceleration > maxAngularAcceleration)
        {
            steeringRotation /= angularAcceleration;
            steeringRotation *= maxAngularAcceleration;
        }

        return steeringRotation;
    }

    public float FaceDynamic(Transform target, float targetRadius, float slowRadius, float maxRotationAngle, float timeToTarget, float maxAngularAcceleration)
    {
        Vector3 direction = target.position - owner.position;
        if (direction.magnitude == 0)
            return 0;

        float orientation = FaceDirection(direction);
        float result = AlignDynamic(orientation, targetRadius, slowRadius, maxRotationAngle, timeToTarget, maxAngularAcceleration);

        return result;
    }

    public float FaceDynamic(Vector3 target, float targetRadius, float slowRadius, float maxRotationAngle, float timeToTarget, float maxAngularAcceleration)
    {
        Vector3 direction = target - owner.position;
        if (direction.magnitude == 0)
            return 0;

        float orientation = FaceDirection(direction);
        float result = AlignDynamic(orientation, targetRadius, slowRadius, maxRotationAngle, timeToTarget, maxAngularAcceleration);

        return result;
    }
}
