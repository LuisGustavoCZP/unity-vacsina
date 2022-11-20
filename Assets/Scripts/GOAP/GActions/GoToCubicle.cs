using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCubicle : GAction
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
        /*GWorld.Instance.GetWorld().ModifyState("TreatingPatient", 1);
        GWorld.AddResource(data.target.GetComponent<GResource>());
        data.agent.inventory.RemoveItem(data.target);
        data.agent.beliefs.ModifyState("stamina", -1);
        int stamina = data.agent.beliefs.GetState("stamina");
        if (stamina == 0)
        {
            data.agent.beliefs.ModifyState("exhausted", 1);
        }*/
        return true;
    }
}
