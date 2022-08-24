using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockHandler : MonoBehaviour
{
    [SerializeField] private FlockAgent agentPrefab = null;
    [SerializeField] private int agentNumber = 20;
    [SerializeField] private Vector3 flockLimits = new Vector3(5, 5, 5);
    [SerializeField] private float boundingAreaMultiplier = 5;
    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private Transform target = null;
    [SerializeField, Range(0, 5)] private float minSpeed = 1f;
    [SerializeField, Range(0, 5)] private float maxSpeed = 5f;
    [SerializeField, Range(0, 5)] private float minRotationSpeed = 1f;
    [SerializeField, Range(0, 5)] private float maxRotationSpeed = 5f;
    [SerializeField, Range(1, 8)] private float neighbourDistance = 5f;
    private List<GameObject> agents;
    private Bounds bounds;

    public List<GameObject> Agents { get => agents; }
    public float MinSpeed { get => minSpeed; }
    public float MaxSpeed { get => maxSpeed; }
    public float MinRotationSpeed { get => minRotationSpeed; }
    public float MaxRotationSpeed { get => maxRotationSpeed; }
    public float NeighbourDistance { get => neighbourDistance; }
    public Vector3 TargetPosition { get => GetTargetPosition(); }
    public Vector3 FlockLimits { get => flockLimits; }
    public Bounds Bounds { get => bounds; }

    private void Start()
    {
        agents = new List<GameObject>();
        bounds = new Bounds(spawnPoint.position, flockLimits * boundingAreaMultiplier);

        for (int i = 0; i < agentNumber; i++)
        {
            float x = Random.Range(-flockLimits.x, flockLimits.x);
            float y = Random.Range(-flockLimits.y, flockLimits.y);
            float z = Random.Range(-flockLimits.z, flockLimits.z);
            Vector3 spawnPosition = spawnPoint.position + new Vector3(x, y, z);

            FlockAgent agent = Instantiate(agentPrefab, spawnPosition, agentPrefab.transform.rotation);
            agent.name = $"Agent_{i}";
            agent.Init(this);
            agents.Add(agent.gameObject);
        }
    }

    public bool OutOfBounds(Vector3 point)
    {
        return !bounds.Contains(point);
    }

    private Vector3 GetTargetPosition()
    {
        float x = Random.Range(-flockLimits.x, flockLimits.x);
        float y = Random.Range(-flockLimits.y, flockLimits.y);
        float z = Random.Range(-flockLimits.z, flockLimits.z);

        return target.position + new Vector3(x, y, z);
    }
}
