using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "GoToHospital", menuName = "GOAP/GActions/GoToHospital")]
public class GoToHospital : GAction
{
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
        //data.agent.beliefs.ModifyState("atHospital", 1);
        return true;
    }
}
