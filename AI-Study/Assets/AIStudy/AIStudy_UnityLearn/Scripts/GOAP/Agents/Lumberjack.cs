using GOAP;
using UnityEngine;

public class Lumberjack : GOAP.Agent
{
    public float hunger = 0f;
    public float hungerBuildup = 5f;

    private new void Awake()
    {
        base.Awake();
        SubGoal completeWork = new SubGoal("workCompleted", 1, false);
        SubGoal fed = new SubGoal("fed", 1, false);
        goals.Add(completeWork, 3);
        goals.Add(fed, 1);
        beliefs.ModifyState("fed", 1);
    }

    private new void Update()
    {
        base.Update();
        hunger = Mathf.Clamp(hunger + hungerBuildup * Time.deltaTime, 0f, 100f);
        if (hunger >= 100)
        {
            if (beliefs.HasState("fed"))
                beliefs.ModifyState("fed", -1);

            beliefs.ModifyState("hungry", 1);
        }
    }
}
