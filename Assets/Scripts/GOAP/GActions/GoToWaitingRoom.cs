using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "GoToWaitingRoom", menuName = "GOAP/GActions/GoToWaitingRoom")]
public class GoToWaitingRoom : GAction
{
    public override bool PrePerform(GActionData data)
    {
        /*if (targetTag != "")
        {
            data.target = GameObject.FindWithTag(targetTag);
        }
        if (data.target == null) return false;

        var t = new GameObject("Target of " + this.name);
        var size = data.target.transform.localScale/2;
        Vector3 randomPos = new Vector3(Random.Range(-size.x, size.x), 0, Random.Range(-size.z, size.z));
        t.transform.position = data.target.transform.position + randomPos;
        data.target = t;*/
        return true;
    }
    public override bool Performed(GActionData data)
    {
        if (duration != 0 && duration > data.startTime - Time.time) return false;
        return true;
    }
    public override bool PostPerform(GActionData data)
    {
        /*GWorld.AddResource(data.agent.gameObject.GetComponent<GResource>());
        data.agent.beliefs.ModifyState("atHospital", 1);
        Destroy(data.target);*/
        return true;
    }
}
