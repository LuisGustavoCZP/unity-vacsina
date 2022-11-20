using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GGoal", menuName = "GOAP/GGoal", order = 0)]
public class GGoal : ScriptableObject, IComparable<GGoal>
{
    public GState[] states = new GState[0];
    public int priority = 0;
    public bool remove = false;

    public int CompareTo(GGoal other)
    {
        return priority - other.priority;
    }
}
