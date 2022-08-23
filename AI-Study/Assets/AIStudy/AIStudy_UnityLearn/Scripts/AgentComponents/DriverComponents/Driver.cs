using UnityEngine;

public class Driver : AgentComponent
{
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected float movementSpeed = 10f;
    private Vector3 lastPosition;

    public Vector3 MoveDirection { get; protected set; }
    public float CurrentSpeed { get; protected set; }

    public override void Init(Agent owner)
    {
        base.Init(owner);
        lastPosition = owner.transform.position;
    }

    public override void Tick()
    {
        if (CanMove())
        {
            Move(GetMoveDirection());

            Vector3 velocity = (transform.position - lastPosition) / Time.deltaTime;
            lastPosition = transform.position;
            CurrentSpeed = velocity.magnitude;
        }
    }

    public void Freeze(bool freeze)
    {
        canMove = !freeze;
    }

    public virtual bool CanMove()
    {
        return canMove && IsEnabled;
    }

    // Get the direction this transform is facing.
    protected virtual Vector3 GetMoveDirection()
    {
        return Vector3.zero;
    }

    protected virtual void Move(Vector3 direction)
    {
        transform.Translate(movementSpeed * Time.deltaTime * direction, Space.World);
    }
}
