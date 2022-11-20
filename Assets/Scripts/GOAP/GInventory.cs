using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory
{
    public List<GResource> items = new List<GResource>();

    public void AddItem(GResource i)
    {
        items.Add(i);
    }

    public void RemoveItem(GResource i)
    {
        int indexToRemove = -1;
        foreach (GResource g in items)
        {
            indexToRemove++;
            if (g == i)
            {
                break;
            }
        }
        if (indexToRemove > -1)
            items.RemoveAt(indexToRemove);
    }

    public GResource FindItemWithTag(string tag)
    {
        foreach(GResource i in items)
        {
            if(i._type == tag)
            {
                return i;
            }
        }
        return null;
    }
}
