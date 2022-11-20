using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Operate", menuName = "GOAP/Actions/Operate")]
public class ActionOperate : GAction
{
    public string resourceTag = "";
    public GState condition = null;
    public GState effect = null;
    public bool exitAfter = false;

    public override bool PrePerform(GActionData data)
    {
        var agent = data.agent;
        var machine = agent.inventory.FindItemWithTag(resourceTag) as GMachine;
        data.target = machine.gameObject;
        //Debug.Log("Has interactor");
        machine.operating = true;
        agent.RotateTo(machine.transform.forward);
        return true;
    }

    public override bool Performed (GActionData data)
    {
        var agent = data.agent;
        var machine = agent.inventory.FindItemWithTag(resourceTag) as GMachine;
        if (!machine.interactor) return false;
      
        if(condition) return agent.beliefs.HasState(condition.key) || (GWorld.Instance.GetWorld().HasState(condition.key));
        if(base.Performed(data)) return true;
        return false;
    }

    public override bool PostPerform(GActionData data)
    {
        var inv = data.agent.inventory;
        var machine = inv.FindItemWithTag(resourceTag) as GMachine;
        if (exitAfter)
        {
            inv.RemoveItem(machine);
            GWorld.AddResource(machine);
        }
        machine.operating = false;
        machine.interactor.beliefs.ModifyState(effect.key, effect.value);
        return true;
    }
}
