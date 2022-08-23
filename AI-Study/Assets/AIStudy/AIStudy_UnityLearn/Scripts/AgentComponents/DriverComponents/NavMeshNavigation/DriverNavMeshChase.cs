using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverNavMeshChase : DriverNavMesh
{
    private Driver targetDriver;

    public override void Tick()
    {
        base.Tick();

        SetDestinationPosition(GetTargetPosition());
    }

    private Vector3 GetTargetPosition()
    {
        return target.position;
    }
}
