using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ResourceQueue
{
    public List<GResource> list = new List<GResource>();
    public string tag;
    public string modState;

    public ResourceQueue (string t, string ms, WorldStates w)
    {
        this.tag = t;
        this.modState = ms;
    }

    public void Add (GResource r)
    {
        list.Add(r);
    }

    public GResource Remove ()
    {
        if (list.Count == 0) return null;
        var r = list[0];
        list.RemoveAt(0);
        return r;
    }

    public bool Remove(GResource r)
    {
        if (list.Count == 0) return false;
        return list.Remove(r);
    }
}

[DefaultExecutionOrder(-2)]
public sealed class GWorld : MonoBehaviour
{
    private static GWorld instance = new GWorld();
    private static WorldStates world;
    private static Dictionary<string, ResourceQueue> resources;
    // Start is called before the first frame update
    static GWorld()
    {
        resources = new Dictionary<string, ResourceQueue>();
        world = new WorldStates();
    }

    private GWorld () {}

    public ResourceQueue Resources (string type)
    {
        return resources[type];
    }

    public static bool AddResource (GResource resource)
    {
        var type = resource.type;
        instance.GetWorld().ModifyState($"has-{type}", 1);
        if (resources.ContainsKey(type))
        {
            resources[resource.type].Add(resource);
            return true;
        }
        var r = new ResourceQueue(type, $"has-{type}", world);
        r.Add(resource);
        resources.Add(type, r);
        return true;
    }

    public static GResource RemoveResource (string type)
    {
        if (!resources.ContainsKey(type)) return null;
        instance.GetWorld().ModifyState($"has-{type}", -1);
        return resources[type].Remove();
    }

    public static bool RemoveResource(GResource resource)
    {
        var type = resource.type;
        if (!resources.ContainsKey(type)) return false;
        instance.GetWorld().ModifyState($"has-{type}", -1);
        return resources[type].Remove(resource);
    }

    public static bool RemoveResource(string type, GResource resource)
    {
        if (!resources.ContainsKey(type)) return false;
        instance.GetWorld().ModifyState($"has-{type}", -1);
        return resources[type].Remove(resource);
    }

    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    { 
        return world; 
    }
}
