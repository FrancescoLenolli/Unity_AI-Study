using UnityEngine;

public class Driver : AgentComponent
{
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected bool canRotate = true;
    [SerializeField] protected float movementSpeed = 10f;
    [SerializeField] protected float rotationSpeed = 10f;

    public override void Tick()
    {
        base.Tick();

        if (CanMove()) Move(GetMoveDirection());
        if (CanRotate()) Face(GetRotateDirection());
    }

    public void Freeze(bool freeze)
    {
        canMove = !freeze;
        canRotate = !freeze;
    }

    // Get the direction this transform is facing.
    protected virtual Vector3 GetMoveDirection()
    {
        return Vector3.zero;
    }

    protected virtual Vector3 GetRotateDirection()
    {
        return Vector3.zero;
    }

    protected virtual bool CanMove()
    {
        return canMove && enabled;
    }

    protected virtual bool CanRotate()
    {
        return canRotate;
    }

    protected virtual void Move(Vector3 direction)
    {
        transform.Translate(movementSpeed * Time.deltaTime * direction, Space.World);
    }

    protected virtual void Face(Vector3 target)
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * target, Space.World);
    }
}
