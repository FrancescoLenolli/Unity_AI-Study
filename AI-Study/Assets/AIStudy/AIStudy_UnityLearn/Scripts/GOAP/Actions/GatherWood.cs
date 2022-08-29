namespace GOAP.Actions
{
    public class GatherWood : Action
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            World.GetWorld().ModifyState("Wood", -1);
            return true;
        }
    }
}