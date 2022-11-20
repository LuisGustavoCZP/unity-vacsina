using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "GoRegister", menuName = "GOAP/GActions/GoRegister")]
public class GoRegister : GAction
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
        return true;
    }
}
