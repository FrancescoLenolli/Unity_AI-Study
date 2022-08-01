using UnityEngine;

public class Driver : AgentComponent
{
    [SerializeField] private Transform target = null;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float threshold = .3f;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool autoPilot = false;

    public override void Tick()
    {
        Face(target);

        if (!CanMove())
            return;

        if (Input.GetKeyDown(KeyCode.T))
            autoPilot = !autoPilot;

        Vector3 direction = autoPilot ?
            GetDirection() : GetInput();

        Move(direction);
    }

    private Vector3 GetInput()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        return new Vector3(xAxis, 0, yAxis);
    }

    // Get the direction this transform is facing.
    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        direction = new Vector3(direction.x, 0, direction.z);

        return direction;
    }

    private void Move(Vector3 direction)
    {
        transform.Translate(movementSpeed * Time.deltaTime * direction, Space.World);
    }

    private void Face(Transform target)
    {
        transform.Rotate(0, GetAngleTo(target.position) * rotationSpeed * Time.deltaTime, 0);
    }

    private bool CanMove()
    {
        if (!canMove)
        {
            return false;
        }
        if(autoPilot)
        {
            if(Vector3.Distance
            (transform.position, target.position) < threshold)
            {
                return false;
            }
        }

        return true;
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
