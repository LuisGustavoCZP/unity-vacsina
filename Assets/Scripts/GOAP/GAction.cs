using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class GActionData
{
    public GAction action;
    public GAgent agent;
    public Dictionary<string, int> conditions;
    public Dictionary<string, int> effects;
    public bool running = false;
    public float startTime = 0;
    public float innerTime = 0;
    public GameObject target;

    public GActionData (GAction action, GAgent agent)
    {
        this.action = action;
        this.agent = agent;
        this.conditions = new Dictionary<string, int>();
        this.effects = new Dictionary<string, int>();
        this.running = false;
        this.startTime = Time.time;
        this.innerTime = 0;
        this.target = null;

        if (action.conditions != null)
        {
            foreach (var w in action.conditions)
            {
                this.conditions.Add(w.key, w.value);
            }
        }

        if (action.effects != null)
        {
            foreach (var w in action.effects)
            {
                this.effects.Add(w.key, w.value);
            }
        }
    }

    public virtual bool PrePerform()
    {
        return action.PrePerform(this);
    }

    public virtual bool Performed()
    {
        return action.Performed(this);
    }

    public virtual bool PostPerform()
    {
        return action.PostPerform(this);
    }


}

public abstract class GAction : ScriptableObject
{
    public float cost = 1.0f;
    public float duration = 0;
    public GState[] conditions;
    public GState[] effects;

    public GAction()
    {

    }

    public void OnValidate()
    {
    }

    public string actionName 
    {
        get => name;
    }

    public bool IsAchivable()
    {
        return true;
    }

    public bool IsAchivableGiven(Dictionary<string, int> conditions)
    {
        foreach (var p in this.conditions)
        {
            if (!conditions.ContainsKey(p.key))
            {
                return false;
            }  
        }
        return true;
    }

    public abstract bool PrePerform(GActionData data);

    public virtual bool Performed(GActionData data)
    {
        if (data.innerTime == 0)
        {
            data.innerTime = Time.time;
        }
        if (duration == 0) return true;
        var t = Time.time - data.innerTime;
        //Debug.Log($"{duration} < {t}");
        if (duration < t) return true;
        return false;
    }

    public abstract bool PostPerform(GActionData data);

}
