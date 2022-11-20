using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoTo", menuName = "GOAP/Actions/GoTo")]
public class ActionGoTo : GAction
{
    public string targetTag;
    public GLocation locationState = GLocation.empty;
    public bool clearBefore = true;

    public override bool PrePerform(GActionData data)
    {
        data.target = GameObject.FindWithTag(targetTag);
        return true;
    }

    public override bool Performed(GActionData data)
    {
        return true;
    }

    public override bool PostPerform(GActionData data)
    {
        GAgent agent = data.agent;
        if (locationState != GLocation.empty)
        {
            if (clearBefore && locationState.clearable) 
            {
                agent.beliefs.RemoveState(agent.location.name);
                agent.location = locationState;
            }
            agent.beliefs.ModifyState(locationState.name, 1);
        }
        return true;
    }
}
