using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private Transform target;

    private List<AgentComponent> components = new List<AgentComponent>();

    public Transform Target { get => target; set { target = value; OnTargetChanged?.Invoke(target); } }
    public Action<Transform> OnTargetChanged { get; set; }

    private void Awake()
    {
        components = GetComponents<AgentComponent>().ToList();
        components.ForEach(component => component.Init(this));
    }

    private void Update()
    {
        components.ForEach(component => component.Tick());
    }
}
