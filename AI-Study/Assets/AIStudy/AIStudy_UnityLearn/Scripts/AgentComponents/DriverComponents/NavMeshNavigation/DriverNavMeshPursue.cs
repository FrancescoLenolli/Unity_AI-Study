using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverNavMeshPursue : DriverNavMesh
{
    [SerializeField] private float lookAheadMultiplier = 1f;
    private Driver targetDriver;

    public override void Tick()
    {
        base.Tick();

        SetDestinationPosition(GetTargetPosition());
    }

    private Vector3 GetTargetPosition()
    {
        return Pursue();
    }

    private Vector3 Pursue()
    {
        Vector3 direction = target.position - transform.position;
        float angleToDirection = Vector3.Angle(Owner.transform.forward, Owner.transform.TransformVector(target.transform.forward));
        float angleToTarget = Vector3.Angle(Owner.transform.forward, Owner.transform.TransformVector(direction));

        if(angleToTarget > 90 && angleToDirection < 20 || GetTargetDriver().CurrentSpeed < 0.01f)
        {
            return targetPosition;
        }

        float lookAhead = (direction.magnitude / 
            (movementSpeed + GetTargetDriver().CurrentSpeed)) * lookAheadMultiplier;

        return target.position + lookAhead * target.forward;
    }

    private Driver GetTargetDriver()
    {
        if (!targetDriver)
            targetDriver = target.GetComponent<Driver>();

        if (targetDriver.Owner.transform != target)
        {
            target = targetDriver.Owner.transform;
            targetDriver = target.GetComponent<Driver>();
        }

        return targetDriver;
    }
}
