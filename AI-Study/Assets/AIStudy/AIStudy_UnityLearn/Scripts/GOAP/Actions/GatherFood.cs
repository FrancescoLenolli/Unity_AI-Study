using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP.Actions
{
    public class GatherFood : Action
    {
        public override bool PrePerform()
        {
            return true;
        }
        public override bool PostPerform()
        {
            return true;
        }
    }
}