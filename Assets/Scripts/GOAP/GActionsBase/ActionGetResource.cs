using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Get", menuName = "GOAP/Actions/GetResource")]
public class ActionGetResource : GAction
{
    public string resourceTag = "";
    public GState effect = null; 
    public bool isOperator = false;

    public override bool PrePerform(GActionData data)
    {
        var resource = GWorld.RemoveResource(resourceTag);
        if (resource == null)
            return false;

        data.target = resource.gameObject;
        data.agent.inventory.AddItem(resource);
        return true;
    }

    public override bool Performed (GActionData data)
    {
        if (base.Performed(data)) return true;
        return false;
    }

    public override bool PostPerform(GActionData data)
    {
        if(effect)
        {
            data.agent.beliefs.ModifyState(effect.key, effect.value);
        }
        return true;
    }
}
