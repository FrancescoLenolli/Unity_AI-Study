using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementType : MonoBehaviour
{
    protected float maxSpeed = 1.0f;
    protected Transform target = null;
    protected Direction directions;
    protected Rotation rotations;

    public void SetBaseValues(Transform target, float maxSpeed)
    {
        this.target = target;
        this.maxSpeed = maxSpeed;
        directions = new Direction(transform);
        rotations = new Rotation(transform);
    }

    public virtual void InitData() { }
    public virtual void ResetData() { }
    public virtual Steering GetSteering() { return null; }


    protected Vector3 GetMovementDirection(Transform target)
    {
        return target.position - transform.position;
    }
    protected Vector3 GetMovementDirection(Vector3 targetPosition)
    {
        return targetPosition - transform.position;
    }


    protected float FaceDirection(Vector3 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}
