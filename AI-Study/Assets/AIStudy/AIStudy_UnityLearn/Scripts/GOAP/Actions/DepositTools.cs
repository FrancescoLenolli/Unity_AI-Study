namespace GOAP.Actions
{
    public class DepositTools : Action
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            World.GetWorld().ModifyState("Tools", 1);
            return true;
        }
    }
}