using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentComponent : MonoBehaviour
{
    [HideInInspector] public Agent owner;

    public virtual void Tick() { }
}
