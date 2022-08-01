using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private List<AgentComponent> components = new List<AgentComponent>();

    private void Awake()
    {
        components = GetComponents<AgentComponent>().ToList();
        components.ForEach(component => component.owner = this);
    }

    private void Update()
    {
        components.ForEach(component => component.Tick());
    }

    public AgentComponent GetAgentComponent(Type type)
    {
        AgentComponent component = components.Where
            (component => component.GetType() == type).First();

        if(component == null)
        {
            Debug.Log($"{name} has no {type} component!");
        }
        return component;
    }
}
