using GOAP;

public class Lumberjack : GOAP.Agent
{
    private new void Awake()
    {
        base.Awake();
        SubGoal subGoal = new SubGoal("workCompleted", 1, false);
        goals.Add(subGoal, 3);
    }
}
