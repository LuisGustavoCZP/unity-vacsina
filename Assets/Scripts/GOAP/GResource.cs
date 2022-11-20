using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[SelectionBase]
public class GResource : MonoBehaviour
{
    public string _type = "";
    public bool started = false;

    public virtual string type
    {
        get { 
            if(enabled) return _type;
            return "Untagged";
        }
    }

    public bool isEnabled
    {
        get { return enabled; }
        set { 
            base.enabled = value;
            UpdateStatus();
        }
    }

    public virtual void UpdateStatus ()
    {
        gameObject.tag = type;
    }

    protected virtual void OnValidate()
    {
        UpdateStatus();
    }

    protected virtual void OnEnable()
    {
        UpdateStatus();
    }

    protected virtual void OnDisable()
    {
        UpdateStatus();
    }

    protected virtual void Start()
    {
        started = true;
    }
}
