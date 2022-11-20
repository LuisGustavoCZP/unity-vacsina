using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatient : GAction
{
    public override bool PrePerform(GActionData data)
    {
        /*var resourcePatient = GWorld.RemoveResource("Patient");
        if (data.target == null)
            return false;

        var resourceCubicle = GWorld.RemoveResource("Cubicle");
        if (resourceCubicle != null)
        {
            data.agent.inventory.AddItem(resourcePatient);
            data.agent.inventory.AddItem(resourceCubicle);
            data.target = resourcePatient.gameObject;
        }
        else
        {
            GWorld.AddResource(data.target.GetComponent<GResource>());
            data.target = null;
            return false;
        }*/
        return true;
    }
    public override bool Performed(GActionData data)
    {
        if (duration != 0 && duration > data.startTime - Time.time) return false;
        return true;
    }
    public override bool PostPerform(GActionData data)
    {
        /*if (data.target)
        {
            var resourceCubicle = data.agent.inventory.FindItemWithTag("Cubicle");
            data.target.GetComponent<GAgent>().inventory.AddItem(resourceCubicle);
        }*/
        return true;
    }
}
