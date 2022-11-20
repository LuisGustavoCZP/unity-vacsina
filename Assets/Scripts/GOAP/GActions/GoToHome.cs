using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHome : GAction
{
    public override bool PrePerform(GActionData data)
    {
        //data.agent.beliefs.RemoveState("atHospital");
        return true;
    }
    public override bool Performed(GActionData data)
    {
        if (duration != 0 && duration > data.startTime - Time.time) return false;
        return true;
    }
    public override bool PostPerform(GActionData data)
    {
        //Destroy(data.agent.gameObject);
        return true;
    }
}
