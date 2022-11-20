using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : GAgent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        /*SubGoal s0 = new SubGoal("relieved", 1, false);
        goals.Add(s0, 10);

        SubGoal s1 = new SubGoal("research", 1, false);
        goals.Add(s1, 1);

        SubGoal s2 = new SubGoal("rested", 1, false);
        goals.Add(s2, 5);*/

        beliefs.ModifyState("stamina", 10);
    }
}
