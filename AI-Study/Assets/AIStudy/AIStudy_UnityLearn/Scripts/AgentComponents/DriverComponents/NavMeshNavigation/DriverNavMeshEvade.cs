using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverNavMeshEvade : DriverNavMesh
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
        return Evade();
    }

    private Vector3 Evade()
    {
        Vector3 direction = target.position - transform.position;

        float lookAhead = (direction.magnitude /
            (movementSpeed + GetTargetDriver().CurrentSpeed)) * lookAheadMultiplier;

        return (Owner.transform.position - (Owner.Target.position - Owner.transform.position)) + lookAhead * target.forward;
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
