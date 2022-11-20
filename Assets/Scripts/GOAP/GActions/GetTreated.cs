using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTreated : GAction
{
    public override bool PrePerform(GActionData data)
    {
        /*data.target = data.agent.inventory.FindItemWithTag("Cubicle");
        if (data.target == null)
            return false;*/
        return true;
    }
    public override bool Performed(GActionData data)
    {
        if (duration != 0 && duration > data.startTime - Time.time) return false;
        return true;
    }
    public override bool PostPerform(GActionData data)
    {
        /*GWorld.Instance.GetWorld().ModifyState("Treated", 1);
        data.agent.beliefs.ModifyState("isCured", 1);
        data.agent.inventory.RemoveItem(data.target);*/
        return true;
    }
}
