using UnityEngine;

public class PatrolFollower : AgentComponent
{
    [SerializeField] private Transform pathParent = null;
    [SerializeField] Waypoint[] path;

    private int index;
    private DriverAutomated driver;

    public override void Init(Agent owner)
    {
        base.Init(owner);

        if (pathParent)
        {
            path = pathParent.GetComponentsInChildren<Waypoint>();
        }

        driver = GetComponent<DriverAutomated>();
        index = 0;
        owner.Target = GetNextWaypoint();

        if(driver)
        driver.OnTargetReached += SetWaypoint;
    }

    private void SetWaypoint()
    {
        Owner.Target = GetNextWaypoint();
    }

    private Transform GetNextWaypoint()
    {

        if (path.Length == 0)
            return null;

        Transform target = path[index].transform;
        index = (index + 1) % path.Length;

        return target;
    }
}
