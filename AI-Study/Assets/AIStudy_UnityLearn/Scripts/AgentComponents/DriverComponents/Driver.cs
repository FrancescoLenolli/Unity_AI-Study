using UnityEngine;

public class Driver : AgentComponent
{
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected float movementSpeed = 10f;
    [SerializeField] protected float rotationSpeed = 10f;

    public override void Tick()
    {
        if (!CanMove())
            return;

        Face(GetRotateDirection());
        Move(GetMoveDirection());
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
        return canMove;
    }

    protected void Move(Vector3 direction)
    {
        transform.Translate(movementSpeed * Time.deltaTime * direction, Space.World);
    }

    protected void Face(Vector3 target)
    {
        transform.Rotate(target);
    }

    protected Vector3 GetAutomatedDirection(Vector3 target)
    {
        return new Vector3(0, GetAngleTo(target) * rotationSpeed * Time.deltaTime, 0);
    }

    /*
    * Get Angle between this transform direction and target transform.
    * Use dot product to get the Angle value, then use Cross Product 
    * to get its sign, so that we know whether to rotate clockwise or not.
    * All those operations can be done with Vector3.SignedAngle.
    */
    private float GetAngleTo(Vector3 target)
    {
        float angle = 0f;
        Vector3 start = transform.forward;
        Vector3 end = target - transform.position;

        Debug.DrawRay(transform.position, start * 10, Color.green);
        Debug.DrawRay(transform.position, end, Color.red);

        /*
         * Vector3.Angle and SignedAngle are equal to dot and acos operations.
         * Angle returns the absolute value of the angle, SignedAngle returns
         * the true value, so that we know where to turn.
         */
        //float dot = Vector3.Dot(start, end);
        //angle = Mathf.Acos(dot / (start.magnitude * end.magnitude
        //angle = Vector3.Angle(start, end);
        angle = Vector3.SignedAngle(start, end, Vector3.up);

        return angle;
    }

    // Vector3.SignedAngle() already performs a Cross Product operation
    // Returns 1 if the Driver needs to turn clockwise to face the target
    // Returns -1 if the Driver needs to turn counter-clockwise to face the target
    private int GetDirectionTo(Vector3 target)
    {
        Vector3 cross = Vector3.Cross(transform.forward, target);
        return cross.y > 0 ? 1 : -1;
    }
}
