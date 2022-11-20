using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMachine : GResource
{
    public GameObject[] activeGOs = new GameObject[0];
    [SerializeField]
    protected bool _operating = false;

    public GameObject interactionPoint = null;
    public GAgent interactor = null;

    public override string type
    {
        get
        {
            if(!enabled) return "Untagged";
            if (operating) return _type;
            return $"Empty{_type}";
        }
    }

    public bool operating
    {
        get => _operating;
        set 
        {
            var op = _operating;
            if(_operating != value)
            {
                GWorld.RemoveResource(this);
            }
            _operating = value;
            if(op != value)
            {
                GWorld.AddResource(this);
            }
            UpdateStatus();
        }
    }

    public override void UpdateStatus()
    {
        base.UpdateStatus();
        foreach (GameObject go in activeGOs)
        {
            go.SetActive(enabled);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GWorld.RemoveResource(this);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GWorld.AddResource(this);
    }
}
