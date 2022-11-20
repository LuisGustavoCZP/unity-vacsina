using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor : GAgent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        /*SubGoal s0 = new SubGoal("relieved", 1, false);
        goals.Add(s0, 10);

        SubGoal s1 = new SubGoal("clean", 1, false);
        goals.Add(s1, 3);

        SubGoal s2 = new SubGoal("rested", 1, false);
        goals.Add(s2, 5);*/

        beliefs.ModifyState("stamina", 5);
    }
}
