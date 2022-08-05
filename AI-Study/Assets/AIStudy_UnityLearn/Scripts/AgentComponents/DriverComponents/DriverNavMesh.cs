using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DriverNavMesh : Driver
{
    private NavMeshAgent navigation;
    private Transform target;
    private Vector3 targetPosition;

    public override void Init(Agent owner)
    {
        base.Init(owner);

        navigation = Owner.GetComponent<NavMeshAgent>();
        target = owner.Target;
        owner.OnTargetChanged += SetDestination;
        owner.OnTargetPositionChanged += SetDestination;
    }

    public override void Tick()
    {
        navigation.isStopped = !CanMove();
    }

    protected override bool CanMove()
    {
        return base.CanMove();
    }

    [ContextMenu("Set Destination")]
    public void SetDestination()
    {
        SetDestination(target);
    }

    public void SetDestination(Transform destination)
    {
        if (!destination)
            return;

        target = destination;
        navigation.SetDestination(target.position);
    }

    public void SetDestination(Vector3 destination)
    {
        targetPosition = destination;
        navigation.SetDestination(targetPosition);
    }

}
