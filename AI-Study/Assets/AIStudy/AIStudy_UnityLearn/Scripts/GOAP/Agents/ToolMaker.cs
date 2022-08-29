using GOAP;

public class ToolMaker : GOAP.Agent
{
    private new void Awake()
    {
        base.Awake();
        SubGoal subGoal = new SubGoal("isRested", 1, false);
        goals.Add(subGoal, 3);
    }
}
