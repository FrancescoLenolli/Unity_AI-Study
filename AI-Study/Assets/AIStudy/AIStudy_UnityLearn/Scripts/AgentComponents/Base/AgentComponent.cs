using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentComponent : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;

    public Agent Owner { get; private set; }
    public bool IsEnabled { get => isEnabled; }

    public virtual void Init(Agent owner) { Owner = owner; }
    public virtual void Tick() { }
    public void Enable(bool enable) { isEnabled = enable; }
}
