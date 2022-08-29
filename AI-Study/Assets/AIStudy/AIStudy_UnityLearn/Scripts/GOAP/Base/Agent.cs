using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GOAP
{
    public class SubGoal
    {
        public Dictionary<string, int> goals;
        public bool remove;

        public SubGoal(string key, int value, bool remove)
        {
            goals = new Dictionary<string, int>();
            goals.Add(key, value);
            this.remove = remove;
        }
    }

    public class Agent : MonoBehaviour
    {
        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
        public Action currentAction;
        public List<Action> actions = new List<Action>();

        protected WorldStates beliefs;

        private Planner planner;
        private Queue<Action> actionQueue;
        private SubGoal currentGoal;
        private Animator animator;
        private bool invoked;

        public WorldStates Beliefs { get => beliefs; }

        public void Awake()
        {
            invoked = false;
            animator = GetComponent<Animator>();
            actions = GetComponents<Action>().ToList();
            beliefs = new WorldStates();
        }

        public void Update()
        {
            if(currentAction != null && currentAction.IsRunning)
            {
                float distance = Vector3.Distance(currentAction.Target.transform.position, transform.position);
                if (currentAction.Agent.hasPath && distance /*currentAction.Agent.remainingDistance*/ < .6f)
                {
                    if (!invoked)
                    {
                        Invoke("CompleteAction", currentAction.Duration);
                        animator.SetTrigger("idle");
                        invoked = true;
                    }
                }

                return;
            }

            if (planner == null || actionQueue == null)
            {
                planner = new Planner(name);

                var sortedGoals = goals.OrderByDescending(goal => goal.Value);
                foreach(KeyValuePair<SubGoal, int> goal in sortedGoals)
                {
                    actionQueue = planner.Plan(actions, goal.Key.goals, beliefs);
                    {
                        if(actionQueue != null)
                        {
                            currentGoal = goal.Key;
                            break;
                        }
                    }
                }
            }

            if(actionQueue != null && actionQueue.Count == 0)
            {
                if(currentGoal.remove)
                {
                    goals.Remove(currentGoal);
                }

                planner = null;
            }

            if(actionQueue != null && actionQueue.Count > 0)
            {
                currentAction = actionQueue.Dequeue();
                if(currentAction.PrePerform())
                {
                    if (currentAction.Target == null && currentAction.TargetName != "")
                    {
                        currentAction.SetTarget(currentAction.TargetName);
                    }

                    if(currentAction.Target != null)
                    {
                        currentAction.Run(true);
                        currentAction.Agent.SetDestination(currentAction.Target.transform.position);
                    }

                    animator.SetTrigger("walk");
                }
                else
                {
                    actionQueue = null;
                }
            }

        }

        private void CompleteAction()
        {
            currentAction.Run(false);
            currentAction.PostPerform();
            invoked = false;
        }
    }
}