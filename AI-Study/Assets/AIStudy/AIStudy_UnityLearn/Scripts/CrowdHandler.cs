using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowdHandler : MonoBehaviour
{
    [SerializeField] private Agent agent = null;
    [SerializeField] private int agentNumber = 10;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float fleeRadius = 5f;
    private List<DriverNavMesh> drivers;
    private List<Transform> targets;
    private float startingSpeed;
    private float startingAngularSpeed;

    private void Awake()
    {
        drivers = new List<DriverNavMesh>();
        targets = new List<Transform>();

        var targetObjects = GameObject.FindGameObjectsWithTag("target");
        foreach (var targetObject in targetObjects)
        {
            targets.Add(targetObject.transform);
        }

        for (int i = 0; i < agentNumber; i++)
        {
            Agent agent = Instantiate(this.agent, targets[Random.Range(0, targets.Count)].position, Quaternion.identity);
            agent.Init();

            DriverNavMesh driver = agent.GetAgentComponent<DriverNavMesh>();
            if (driver) drivers.Add(driver);
            if (i == 0)
            {
                startingSpeed = driver.Navigation.speed;
                startingAngularSpeed = driver.Navigation.angularSpeed;
            }

            Animator animator = agent.GetComponent<Animator>();
            animator.SetFloat("walkOffset", Random.Range(0f, .6f));
            animator.SetFloat("walkSpeedMultiplier", Random.Range(.5f, 1.5f));
        }

        InvokeRepeating("SetDangerPoint", 3f, 1f);
    }

    private void Update()
    {
        foreach (DriverNavMesh driver in drivers)
        {
            driver.Owner.Tick();

            if (driver.Navigation.remainingDistance < 1f)
            {
                if (driver.Navigation.speed != startingSpeed)
                    ResetDriver(driver);

                driver.SetDestinationTarget(targets[Random.Range(0, targets.Count)]);
            }
        }
    }

    private void ResetDriver(DriverNavMesh driver)
    {
        driver.Navigation.speed = startingSpeed;
        driver.Navigation.angularSpeed = startingAngularSpeed;
        driver.SetDestinationTarget(targets[Random.Range(0, targets.Count)]);
    }

    private void FleeFrom(DriverNavMesh fleeingDriver, Vector3 position)
    {
        if (!(Vector3.Distance(position, fleeingDriver.Owner.transform.position) < detectionRadius))
            return;

        Vector3 fleeDirection = (fleeingDriver.Owner.transform.position - position).normalized;
        Vector3 newGoal = fleeingDriver.Owner.transform.position + fleeDirection * fleeRadius;

        NavMeshPath path = new NavMeshPath();
        fleeingDriver.Navigation.CalculatePath(newGoal, path);

        if (path.status != NavMeshPathStatus.PathInvalid)
        {
            fleeingDriver.SetDestinationPosition(path.corners[path.corners.Length - 1]);
            fleeingDriver.Navigation.speed = fleeingDriver.Navigation.speed * 3;
            fleeingDriver.Navigation.angularSpeed = fleeingDriver.Navigation.angularSpeed * 3;
        }
    }

    private void SetDangerPoint()
    {
        Vector3 dangerPoint = targets[Random.Range(0, targets.Count)].position;
        drivers.ForEach(driver => FleeFrom(driver, dangerPoint));
    }
}
