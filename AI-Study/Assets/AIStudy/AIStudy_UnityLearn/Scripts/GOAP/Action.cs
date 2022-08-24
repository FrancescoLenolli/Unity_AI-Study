using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP
{
    public abstract class Action : MonoBehaviour
    {
        [SerializeField] protected string actionName = "Action";
        [SerializeField] protected float cost = 1f;
        [SerializeField] protected GameObject target = null;
        [SerializeField] protected GameObject targetTag = null;
        [SerializeField] protected float duration = 1f;
        [SerializeField] protected WorldState[] preConditions;
        [SerializeField] protected WorldState[] afterEffects;
        [SerializeField] protected bool isRunning = false;
        protected NavMeshAgent agent;
        protected Dictionary<string, int> preconditions;
        protected Dictionary<string, int> effects;

        public Action()
        {
            preconditions = new Dictionary<string, int>();
            effects = new Dictionary<string, int>();
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

            if(preConditions != null)
            {
                foreach(WorldState w in preConditions)
                {
                    preconditions.Add(w.key, w.value);
                }
            }

            if (afterEffects != null)
            {
                foreach (WorldState w in afterEffects)
                {
                    effects.Add(w.key, w.value);
                }
            }
        }

        public bool IsAchievable()
        {
            return true;
        }

        public bool IsAchievableGiven(Dictionary<string, int> conditions)
        {
            foreach(KeyValuePair<string, int> condition in preconditions)
            {
                if (!conditions.ContainsKey(condition.Key))
                    return false;
            }

            return true;
        }

        public abstract void PrePerform();
        public abstract void PostPerform();
    }
}
