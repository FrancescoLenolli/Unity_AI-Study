using System;
using UnityEngine;

public class DriverAutomated : Driver
{
    [SerializeField] private float threshold = .3f;
    private  Transform target = null;

    public Action OnTargetReached { get; set; }

    public override void Init(Agent owner)
    {
        base.Init(owner);
        target = owner.Target;
        owner.OnTargetChanged += (target) => this.target = target; 
    }

    protected override bool CanMove()
    {
        bool canMove = base.CanMove();

        bool isOnTarget = !target ? true : Vector3.Distance
            (transform.position, target.position) < threshold;

        if(isOnTarget)
        {
            OnTargetReached?.Invoke();
        }

        return !isOnTarget && canMove;
    }

    protected override Vector3 GetMoveDirection()
    {
        Vector3 direction = transform.forward;
        direction = new Vector3(direction.x, 0, direction.z);

        return direction;
    }

    protected override Vector3 GetRotateDirection()
    {
        return GetAutomatedDirection(target.position);
    }
}
