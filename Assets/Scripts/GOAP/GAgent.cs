using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

/*public class SubGoal
{
    public Dictionary<string, int> sgoals;
    public bool remove;

    public SubGoal (string s, int i, bool r)
    {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(s, i);
        remove = r;
    }
}*/

[System.Serializable]
public struct GLocation
{
    public string name;
    public bool clearable;

    public GLocation(string name)
    {
        this.name = name;
        clearable = true;
    }

    public GLocation(string name, bool clearable)
    {
        this.name = name;
        this.clearable = clearable;
    }

    public static GLocation empty 
    {
        get => new GLocation("");
    }

    public static bool operator == (GLocation a, GLocation b)
    {
        return a.name == b.name && a.clearable == b.clearable;
    }
    public static bool operator != (GLocation a, GLocation b)
    {
        return a.name != b.name || a.clearable != b.clearable;
    }
}

[SelectionBase]
public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public List<GGoal> goals = new List<GGoal>();
    public Dictionary<GGoal, int> goalSet = new Dictionary<GGoal, int>();
    public GInventory inventory = new GInventory();
    public WorldStates beliefs = new WorldStates();

    GPlanner planner;
    Queue<GAction> actionQueue;
    public GActionData currentAction;
    GGoal currentGoal;

    public NavMeshAgent navMesh;
    public GLocation location = GLocation.empty;
    public Vector3 destination = Vector3.zero;
    public Vector3 direction = Vector3.forward;
    public bool rotating = false;

    protected virtual void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Invoke("NeedToilet", Random.Range(5, 40));
        foreach (var goal in goals)
        {
            goalSet.Add(goal, goal.priority);
        }
    }

    // Update is called once per frame
    protected virtual void LateUpdate()
    {
        if(currentAction != null && currentAction.running)
        {
            var remainDistance = Vector3.Distance(destination, this.transform.position);
            if(remainDistance < 1f)
            {
                if(currentAction.Performed())
                {
                    currentAction.running = false;
                    currentAction.PostPerform();
                }
            }
            return;
        }

        if(planner == null || actionQueue == null)
        {
            planner = new GPlanner();

            var sortedGoals = from entry in goalSet orderby entry.Value descending select entry;
            
            foreach(var sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key, beliefs);
                if(actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        if(actionQueue != null && actionQueue.Count == 0)
        {
            if(currentGoal.remove)
            {
                goalSet.Remove(currentGoal);
            }
            planner = null;
        }

        if(actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = new GActionData(actionQueue.Dequeue(), this);
            if(currentAction.PrePerform())
            {
                if(currentAction.target != null)
                {
                    destination = currentAction.target.transform.position;

                    Transform dest = currentAction.target.transform.Find("Destination");
                    if (dest != null)
                        destination = dest.position;

                    SetDestination(destination);
                }
                currentAction.running = true;
            }
            else
            {
                actionQueue = null;
            }
        }
    }

    public bool SetDestination (Vector3 pos)
    {
        return navMesh.SetDestination(pos);
    }

    IEnumerator RotationCoroutine ()
    {
        if(rotating) yield break;
        rotating = true;

        var d = Vector3.Distance(transform.forward, direction);
        while (d > .1)
        {
            var dir = Vector3.Slerp(this.transform.forward, direction, Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(dir, transform.up);
            yield return null;
            d = Vector3.Distance(transform.forward, direction);
        }

        rotating = false;
        yield break;
    }

    public bool RotateTo (Vector3 dir)
    {
        direction = dir;
        if(!rotating) StartCoroutine(RotationCoroutine());
        return true;
    }

    void NeedToilet()
    {
        beliefs.ModifyState("needToilet", 1);
    }
}
