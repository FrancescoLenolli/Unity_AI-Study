using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : AgentComponent
{
    [SerializeField] private GameObject startingNode = null;
    [SerializeField] private Transform[] path = null;

    private int index;
    private DriverAutomated driver;

    public GameObject StartingNode { get => startingNode; }

    public override void Init(Agent owner)
    {
        base.Init(owner);

        driver = GetComponent<DriverAutomated>();
        index = 0;
        owner.Target = GetWaypoint();

        if (driver)
            driver.OnTargetReached += SetNewWaypoint;

        owner.transform.position = startingNode.transform.position;
    }

    public void SetPath(Transform[] path)
    {
        index = 0;
        this.path = path;
        owner.Target = GetWaypoint();
    }

    private void SetNewWaypoint()
    {
        owner.Target = GetNextWaypoint();
    }

    private Transform GetNextWaypoint()
    {
        if (path.Length == 0 || index >= path.Length)
            return null;

        Transform target = path[index].transform;
        index++;

        return target;
    }

    private Transform GetWaypoint()
    {
        if (path.Length == 0)
            return null;

        Transform target = path[index].transform;
        return target;
    }
}
