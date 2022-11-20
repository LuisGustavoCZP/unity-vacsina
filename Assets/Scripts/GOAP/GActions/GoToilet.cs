using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToilet : GAction
{
    Vector3 lastDestination;
    public override bool PrePerform(GActionData data)
    {
        /*lastDestination = data.agent.destination;
        data.target = GWorld.RemoveResource("toilets").gameObject;
       
        if (data.target == null)
        {
            return false;
        }
        data.agent.inventory.AddItem(data.target);*/
        return true;
    }
    public override bool Performed(GActionData data)
    {
        if (duration != 0 && duration > data.startTime - Time.time) return false;
        return true;
    }
    public override bool PostPerform(GActionData data)
    {
        /*GWorld.AddResource(data.target.GetComponent<GResource>());
        data.agent.inventory.RemoveItem(data.target);
        data.agent.beliefs.RemoveState("needToilet");
        data.agent.Invoke("NeedToilet", Random.Range(20, 40));
        data.agent.SetDestination(lastDestination);*/
        return true;
    }
}
