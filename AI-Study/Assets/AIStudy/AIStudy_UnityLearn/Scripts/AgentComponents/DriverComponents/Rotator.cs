using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : AgentComponent
{
    [SerializeField] private bool canRotate = true;
    [SerializeField] protected float rotationSpeed = 10f;

    public override void Tick()
    {
        base.Tick();

        Rotate(GetRotation());
    }

    public void Freeze(bool freeze)
    {
        canRotate = !freeze;
    }

    public virtual bool CanRotate()
    {
        return canRotate && IsEnabled;
    }

    protected virtual Vector3 GetRotation() { return Vector3.zero; }

    protected virtual void Rotate(Vector3 value) { }
}
