using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverNavMeshFlee : DriverNavMesh
{
    public override void Tick()
    {
        base.Tick();

        SetDestinationPosition(GetFleePosition());
    }

    private Vector3 GetFleePosition()
    {
        return Owner.transform.position - (Owner.Target.position - Owner.transform.position);
    }
}
