using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "CleanUpPuddle", menuName = "GOAP/GActions/CleanUpPuddle")]
public class CleanUpPuddle : GAction
{
    public override bool PrePerform(GActionData data)
    {
        /*data.target = GWorld.RemoveResource("puddles").gameObject;
       
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
        /*data.agent.inventory.RemoveItem(data.target);
        Destroy(data.target);*/
        return true;
    }
}
