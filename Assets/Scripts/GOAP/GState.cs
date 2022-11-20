using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    String = 0,
    Int = 1,
    Float = 2,
    Boolean = 3
}

public struct StateData
{
    public StateType type;
    public string stringValue;
    public int intValue;
    public float floatValue;
    public bool boolValue;

    public StateData(string s)
    {
        type = StateType.String;
        stringValue = s;
        intValue = 0;
        floatValue = 0;
        boolValue = false;
    }

    public StateData(int i)
    {
        type = StateType.Int;
        stringValue = "";
        intValue = i;
        floatValue = 0;
        boolValue = false;
    }

    public StateData(float f)
    {
        type = StateType.Float;
        stringValue = "";
        intValue = 0;
        floatValue = f;
        boolValue = false;
    }

    public StateData(bool b)
    {
        type = StateType.Boolean;
        stringValue = "";
        intValue = 0;
        floatValue = 0;
        boolValue = b;
    }
}


[CreateAssetMenu(fileName = "GState", menuName = "GOAP/GState", order = 0)]
public class GState : ScriptableObject
{
    public string key = "";
    public int value = 0;
}
