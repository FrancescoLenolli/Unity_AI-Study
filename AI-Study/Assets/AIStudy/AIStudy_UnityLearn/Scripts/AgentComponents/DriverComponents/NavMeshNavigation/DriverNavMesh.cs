using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DriverNavMesh : Driver
{
    protected Transform target;
    protected Vector3 targetPosition;
    protected NavMeshAgent navigation;

    public NavMeshAgent Navigation { get => navigation; }

    public override void Init(Agent owner)
    {
        base.Init(owner);

        navigation = Owner.GetComponent<NavMeshAgent>();
        target = owner.Target;
        owner.OnTargetChanged += SetDestinationTarget;
        owner.OnTargetPositionChanged += SetDestinationPosition;
    }

    public override void Tick()
    {
        navigation.isStopped = !CanMove();
    }

    public void SetDestinationTarget(Transform destination)
    {
        if (!destination)
            return;

        target = destination;
        navigation.SetDestination(target.position);
    }

    public void SetDestinationPosition(Vector3 destination)
    {
        if (destination == Vector3.zero)
            return;

        targetPosition = destination;
        navigation.SetDestination(targetPosition);
    }

}
