using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GOAP
{
    public class Node
    {
        public Node parent;
        public float cost;
        public Dictionary<string, int> state;
        public Action action;

        public Node(Node parent, float cost, Dictionary<string, int> allStates, Action action)
        {
            this.parent = parent;
            this.cost = cost;
            state = new Dictionary<string, int>(allStates);
            this.action = action;
        }

        public Node(Node parent, float cost, Dictionary<string, int> allStates, Dictionary<string, int> beliefStates, Action action)
        {
            this.parent = parent;
            this.cost = cost;
            this.action = action;
            state = new Dictionary<string, int>(allStates);
            foreach(KeyValuePair<string, int> belief in beliefStates)
            {
                if(!state.ContainsKey(belief.Key))
                {
                    state.Add(belief.Key, belief.Value);
                }
            }
        }
    }

    public class Planner
    {
        private string agentName;

        public Planner(string agentName)
        {
            this.agentName = agentName;
        }

        public Queue<Action> Plan(List<Action> actions, Dictionary<string, int> goal, WorldStates beliefStates)
        {
            List<Action> availableActions = actions.TakeWhile(action => action.IsAchievable()).ToList();

            List<Node> leaves = new List<Node>();
            Node startingNode = new Node(null, 0, World.GetWorld().GetStates(), beliefStates.states, null);

            bool planFound = BuildGraph(startingNode, leaves, availableActions, goal);
            if (!planFound)
            {
                Debug.LogWarning($"{agentName} has no Plan!");
                return null;
            }

            Node cheapestNode = null;
            foreach (Node leaf in leaves)
            {
                if (cheapestNode == null)
                {
                    cheapestNode = leaf;
                    continue;
                }

                if (leaf.cost < cheapestNode.cost)
                    cheapestNode = leaf;
            }

            List<Action> result = new List<Action>();
            Node node = cheapestNode;

            while(node != null)
            {
                if(node.action != null)
                {
                    result.Insert(0, node.action);
                }

                node = node.parent;
            }

            Queue<Action> queue = new Queue<Action>(result);
            string planLog = $"{agentName} Plan is:\n";

            int index = 1;
            foreach(Action action in queue)
            {
                planLog += $"{index}. {action.Name}\n";
                ++index;
            }
            Debug.Log(planLog);

            return queue;
         }

        private bool BuildGraph(Node parent, List<Node> leaves, List<Action> availableActions, Dictionary<string, int> goal)
        {
            bool foundPath = false;

            foreach(Action action in availableActions)
            {
                if(action.IsAchievableGiven(parent.state))
                {
                    Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                    foreach(KeyValuePair<string, int> effect in action.Effects)
                    {
                        if (!currentState.ContainsKey(effect.Key))
                            currentState.Add(effect.Key, effect.Value);
                    }

                    Node node = new Node(parent, parent.cost + action.Cost, currentState, action);

                    if(GoalAchieved(goal, currentState))
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        List<Action> subset = ActionSubset(availableActions, action);
                        foundPath = BuildGraph(node, leaves, subset, goal);
                    }
                }
            }

            return foundPath;
        }
        private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
        {
            foreach(KeyValuePair<string, int> g in goal)
            {
                if (!state.ContainsKey(g.Key))
                    return false;
            }

            return true;
        }

        private List<Action> ActionSubset(List<Action> actions, Action toRemove)
        {
            List<Action> subset = new List<Action>();

            foreach(Action action in actions)
            {
                if (!action.Equals(toRemove))
                    subset.Add(action);
            }

            return subset;
        }

    }
}