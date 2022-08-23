using UnityEngine;

public class Sight : AgentComponent
{
    [SerializeField] private float sightAngle = 45f;
    [SerializeField] private float sightDistance = 5f;
    public bool targetOnSight = false;

    public override void Tick()
    {
        base.Tick();

        if (!Owner.Target)
            return;

        Vector3 direction = Owner.Target.position - Owner.transform.position;
        Vector3 opposite = Owner.Target.position - Owner.transform.forward;
        Debug.DrawLine(Owner.transform.position, opposite, Color.blue);
        Debug.DrawLine(Owner.transform.position, direction, Color.red);
        float angle = Vector3.Angle(direction, Owner.transform.forward);

        targetOnSight = angle <= sightAngle / 2 &&  direction.magnitude <= sightDistance;
    }
}
