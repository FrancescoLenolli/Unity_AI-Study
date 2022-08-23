using UnityEngine;

public class RotatorFaceTarget : Rotator
{
    private Transform target;
    private Vector3 targetPosition;

    public override void Init(Agent owner)
    {
        base.Init(owner);
        target = owner.Target;
        targetPosition = owner.TargetPosition;
        owner.OnTargetChanged += (target) => this.target = target;
        owner.OnTargetPositionChanged += (targetPosition) => this.targetPosition = targetPosition;
    }

    protected override void Rotate(Vector3 value)
    {
        transform.Rotate(value);
    }

    protected override Vector3 GetRotation()
    {
        Vector3 rotation;
        Vector3 targetPosition = target ? target.position : this.targetPosition;

        rotation = new Vector3(0, GetAngleTo(targetPosition) * rotationSpeed * Time.deltaTime, 0);
        return rotation;
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
}
