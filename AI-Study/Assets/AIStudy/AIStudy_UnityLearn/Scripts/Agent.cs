using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private bool isIndependent = true;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetPosition;
    private List<AgentComponent> components = new List<AgentComponent>();

    public Transform Target { get => target; set => SetTarget(value); }
    public Vector3 TargetPosition { get => targetPosition; set => SetTargetPosition(value); }
    public Action<Transform> OnTargetChanged { get; set; }
    public Action<Vector3> OnTargetPositionChanged { get; set; }

    private void Awake()
    {
        if (isIndependent) Init();
    }

    private void Update()
    {
        if (isIndependent) Tick();
    }

    public void Init()
    {
        components = GetComponents<AgentComponent>().ToList();
        components.ForEach(component => component.Init(this));
    }

    public void Tick()
    {
        for (int i = 0; i < components.Count; ++i)
        {
            AgentComponent component = components[i];

            if (!component)
            {
                components.Remove(component);
                continue;
            }

            if (component.IsEnabled)
            {
                component.Tick();
            }
        }
    }

    public void AddAgentComponent<T>() where T : AgentComponent
    {
        AgentComponent newComponent = gameObject.AddComponent<T>();
        newComponent.Init(this);
        components.Add(newComponent);
    }

    public void RemoveAgentComponent(AgentComponent component)
    {
        if (components.Find(x => x == component) != null)
        {
            component.Enable(false);
            components.Remove(component);
        }
    }

    public T GetAgentComponent<T>() where T : AgentComponent
    {
        T component = (T)components.TakeWhile(x => x.GetType() == typeof(T)).First();

        return component;
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
