using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentComponent : MonoBehaviour
{
    [HideInInspector] public Agent owner;

    public virtual void Init(Agent owner) { this.owner = owner; }
    public virtual void Tick() { }
}
