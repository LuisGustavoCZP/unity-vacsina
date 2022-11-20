using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaitState", menuName = "GOAP/Actions/WaitState")]
public class ActionWaitState : GAction
{
    public GState state = null;
    public bool self = false;

    public override bool PrePerform(GActionData data)
    {
        return true;
    }

    public override bool Performed(GActionData data)
    {
        return data.agent.beliefs.HasState(state.key) || (!self && GWorld.Instance.GetWorld().HasState(state.key));
    }

    public override bool PostPerform(GActionData data)
    {
        return true;
    }
}
