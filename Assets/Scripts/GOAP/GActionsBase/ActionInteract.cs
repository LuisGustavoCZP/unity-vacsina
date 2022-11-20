using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Interact", menuName = "GOAP/Actions/Interact")]
public class ActionInteract : GAction
{
    public string resourceTag = "";
    public GState condition = null;
    public bool self = false;

    public override bool PrePerform(GActionData data)
    {
        var resource = GWorld.RemoveResource(resourceTag);
        if (resource == null)
            return false;

        if (resource.GetType() == typeof(GMachine)) 
        {
            var machine = resource as GMachine;
            data.target = machine.interactionPoint;
        }
        else data.target = resource.gameObject;

        data.agent.inventory.AddItem(resource);

        return true;
    }

    public override bool Performed (GActionData data)
    {
        var agent = data.agent;
        var resource = agent.inventory.FindItemWithTag(resourceTag);
        if (resource.GetType() == typeof(GMachine)) (resource as GMachine).interactor = data.agent;
        if (condition) return agent.beliefs.HasState(condition.key) || (!self && GWorld.Instance.GetWorld().HasState(condition.key));
        return true;
    }

    public override bool PostPerform(GActionData data)
    {
        var inv = data.agent.inventory;
        var resource = inv.FindItemWithTag(resourceTag);
        inv.RemoveItem(resource);
        GWorld.AddResource(resource);
        if (resource.GetType() == typeof(GMachine))
        {
            var machine = resource as GMachine;
            machine.interactor = null;
        }
        return true;
    }
}
