using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Lumberjack : GOAP.Agent
{
    private void Awake()
    {
        base.Awake();
        SubGoal subGoal = new SubGoal("workCompleted", 1, false);
        goals.Add(subGoal, 3);
    }
}
