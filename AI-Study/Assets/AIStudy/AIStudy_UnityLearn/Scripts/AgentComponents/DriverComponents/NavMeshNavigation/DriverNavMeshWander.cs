using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverNavMeshWander : DriverNavMesh
{
    [SerializeField] float wanderRadius = 10f;
    [SerializeField] float wanderDistance = 10f;
    [SerializeField] float wanderJitter = 10f;
    [SerializeField] float newPositionTimer = 2f;
    private new Vector3 targetPosition;

    public override void Init(Agent owner)
    {
        base.Init(owner);
        InvokeRepeating("Wander", 0f, newPositionTimer);
    }

    public override void Tick()
    {
        base.Tick();

        SetDestinationPosition(GetTargetPosition());
    }

    private Vector3 GetTargetPosition()
    {
        return targetPosition;
    }

    private void Wander()
    {
        float x = Random.Range(-1f, 1f) * wanderJitter;
        float z = Random.Range(-1f, 1f) * wanderJitter;

        Vector3 wanderTarget = new Vector3(x, 0f, z);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = Owner.transform.InverseTransformVector(targetLocal);

        targetPosition = targetWorld;
    }
}
