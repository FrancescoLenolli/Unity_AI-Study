using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP
{
    public abstract class Action : MonoBehaviour
    {
        [SerializeField] private float cost = 1f;
        [SerializeField] private GameObject target = null;
        [SerializeField] private string targetName = null;
        [SerializeField] private float duration = 1f;
        [SerializeField] protected WorldState[] preConditions;
        [SerializeField] protected WorldState[] afterEffects;
        [SerializeField] private bool isRunning = false;
        private NavMeshAgent agent;
        protected Dictionary<string, int> preconditions;
        private Dictionary<string, int> effects;

        public Dictionary<string, int> Effects { get => effects; }
        public float Cost { get => cost; }
        public bool IsRunning { get => isRunning; }
        public NavMeshAgent Agent { get => agent; }
        public float Duration { get => duration; }
        public string TargetName { get => targetName; }
        public GameObject Target { get => target; }

        public Action()
        {
            preconditions = new Dictionary<string, int>();
            effects = new Dictionary<string, int>();
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

            if (preConditions != null)
            {
                foreach (WorldState w in preConditions)
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

        public void Run(bool run)
        {
            isRunning = run;
        }

        public void SetTarget(string targetName)
        {
            target = GameObject.Find(targetName);
        }

        public bool IsAchievableGiven(Dictionary<string, int> conditions)
        {
            foreach (KeyValuePair<string, int> condition in preconditions)
            {
                if (!conditions.ContainsKey(condition.Key))
                    return false;
            }

            return true;
        }

        public abstract bool PrePerform();
        public abstract bool PostPerform();
        public virtual bool IsAchievable() { return true; }
    }
}
