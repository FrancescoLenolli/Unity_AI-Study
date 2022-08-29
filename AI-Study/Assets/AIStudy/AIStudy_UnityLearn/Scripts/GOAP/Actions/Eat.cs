using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

namespace GOAP.Actions
{
    public class Eat : Action
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            World.GetWorld().ModifyState("Food", -1);

            return true;
        }
    }
}