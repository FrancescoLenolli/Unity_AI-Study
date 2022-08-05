using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetPosition;

    private List<AgentComponent> components = new List<AgentComponent>();

    public Transform Target { get => target; set => SetTarget(value); }
    public Vector3 TargetPosition { get => targetPosition; set => SetTargetPosition(value); }

    public Action<Transform> OnTargetChanged { get; set; }
    public Action<Vector3> OnTargetPositionChanged { get; set; }

    private void Awake()
    {
        components = GetComponents<AgentComponent>().ToList();
        components.ForEach(component => component.Init(this));
    }

    private void Update()
    {
        components.ForEach(component => component.Tick());
    }

    private void SetTarget(Transform value)
    {
        target = value;
        OnTargetChanged?.Invoke(target);
    }

    private void SetTargetPosition(Vector3 value)
    {
        targetPosition = value;
        OnTargetPositionChanged?.Invoke(targetPosition);
    }
}
