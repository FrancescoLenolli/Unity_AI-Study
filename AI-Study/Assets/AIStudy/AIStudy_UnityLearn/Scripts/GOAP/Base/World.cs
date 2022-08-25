using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public static class World 
    {
        private static WorldStates world;

        public static WorldStates GetWorld()
        {
            if(world == null)
            {
                world = new WorldStates();
            }

            return world;
        }
    }
}