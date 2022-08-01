using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : AgentComponent
{
    [SerializeField] private Transform target = null;

    public Transform Target { get => target; set => target = value; }
}
