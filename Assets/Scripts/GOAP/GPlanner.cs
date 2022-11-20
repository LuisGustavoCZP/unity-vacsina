using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    public Node (Node parent, float cost, Dictionary<string, int> allstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);
        this.action = action;
    }
    public Node(Node parent, float cost, Dictionary<string, int> allstates, Dictionary<string, int> beliefstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);
        foreach (var b in beliefstates)
        {
            if(!this.state.ContainsKey(b.Key))
            {
                this.state.Add(b.Key, b.Value);
            }
        }

        this.action = action;
    }
}

public class GPlanner
{
    public Queue<GAction> plan(List<GAction> actions, GGoal goal, WorldStates beliefstates)
    {
        List<GAction> usuableActions = new List<GAction>();
        foreach (var a in actions)
        {
            if(a.IsAchivable())
                usuableActions.Add(a);
        }

        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), beliefstates.GetStates(), null);

        bool sucess = BuildGraph(start, leaves, usuableActions, goal);

        if(!sucess)
        {
            //Debug.Log("NO PLAN");
            return null;
        }

        Node cheapest = null;
        foreach (var leaf in leaves)
        {
            if(cheapest == null)
            {
                cheapest = leaf;
            }
            else
            {
                if (leaf.cost < cheapest.cost)
                {
                    cheapest = leaf;
                }
            }
        }

        List<GAction> result = new List<GAction>();
        Node n = cheapest;
        while(n != null)
        {
            if (n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();
        foreach (var a in result)
        {
            queue.Enqueue(a);
        }

        /*Debug.Log("The Plan is:");
        foreach (var a in queue)
        {
            Debug.Log("Q: " + a.actionName);
        }*/

        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usuableActions, GGoal goal)
    {
        bool foundPath = false;
        foreach (var action in usuableActions)
        {
            if(action.IsAchivableGiven(parent.state))
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach (var eff in action.effects)
                {
                    if(!currentState.ContainsKey(eff.key))
                    {
                        currentState.Add(eff.key, eff.value);
                    }
                }

                Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                if(GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GAction> subset = ActionSubset(usuableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if(found)
                    {
                        foundPath = true;
                    }
                }
            }
        }

        return foundPath;
    }

    private bool GoalAchieved(GGoal goal, Dictionary<string, int> state)
    {
        foreach(var g in goal.states)
        {
            if(!state.ContainsKey(g.key))
            {
                return false;
            }
        }
        return true;
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe)
    {
        List<GAction> subset = new List<GAction>();
        foreach(GAction a in actions)
        {
            if(!a.Equals(removeMe))
            {
                subset.Add(a);
            }
        }
        return subset;
    }
}
