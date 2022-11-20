using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoRest : GAction
{
    public int stamina;
    public override bool PrePerform(GActionData data)
    {
        return true;
    }
    public override bool Performed(GActionData data)
    {
        if (duration != 0 && duration > data.startTime - Time.time) return false;
        return true;
    }
    public override bool PostPerform(GActionData data)
    {
        /*data.agent.beliefs.RemoveState("exhausted");
        data.agent.beliefs.ModifyState("stamina", stamina);*/
        return true;
    }
}
