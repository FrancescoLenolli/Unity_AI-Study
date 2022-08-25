using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP.Actions
{
    public class GetWood : Action
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


// This is neat
public static class TestExtension
{
    static T Random<T>(List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}