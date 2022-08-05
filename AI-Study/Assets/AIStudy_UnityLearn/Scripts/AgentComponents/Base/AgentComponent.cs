using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentComponent : MonoBehaviour
{
    [SerializeField] private new bool enabled = true;

    public Agent Owner { get; private set; }
    public bool Enabled { get => enabled; }

    public virtual void Init(Agent owner) { Owner = owner; }
    public virtual void Tick() { if (!Enabled) return; }
    public void Enable(bool enable) { enabled = enable; }
}
