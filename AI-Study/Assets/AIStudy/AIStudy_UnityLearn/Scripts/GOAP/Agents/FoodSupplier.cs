using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class FoodSupplier : Agent
    {
        private new void Awake()
        {
            base.Awake();
            SubGoal subGoal = new SubGoal("foodDeposited", 1, false);
            goals.Add(subGoal, 3);
        }
    }
}