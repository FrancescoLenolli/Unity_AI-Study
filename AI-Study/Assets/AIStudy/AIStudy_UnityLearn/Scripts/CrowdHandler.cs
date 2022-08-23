using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrowdHandler : MonoBehaviour
{
    [SerializeField] private Agent agent = null;
    [SerializeField] private int agentNumber = 10;
    private List<DriverNavMesh> drivers;
    private List<Transform> targets;

    private void Awake()
    {
        drivers = new List<DriverNavMesh>();
        targets = new List<Transform>();

        var targetObjects = GameObject.FindGameObjectsWithTag("target");
        foreach(var targetObject in targetObjects)
        {
            targets.Add(targetObject.transform);
        }
        
        for (int i = 0; i < agentNumber; i++)
        {
            Agent agent = Instantiate(this.agent, targets[Random.Range(0, targets.Count)].position, Quaternion.identity);
            agent.Init();

            DriverNavMesh driver = agent.GetAgentComponent<DriverNavMesh>();
            if (driver) drivers.Add(driver);

            Animator animator = agent.GetComponent<Animator>();
            animator.SetFloat("walkOffset", Random.Range(0f, .6f));
            animator.SetFloat("walkSpeedMultiplier", Random.Range(.5f, 1.5f));
        }
    }

    private void Update()
    {
        foreach (DriverNavMesh driver in drivers)
        {
            driver.Owner.Tick();

            if(driver.Navigation.remainingDistance < 1f)
            {
                driver.SetDestinationTarget(targets[Random.Range(0, targets.Count)]);
            }
        }
    }
}
