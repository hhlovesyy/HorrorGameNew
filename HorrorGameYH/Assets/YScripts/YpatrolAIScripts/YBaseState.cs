using System;
using UnityEngine;

public abstract class YBaseState 
{
    protected GameObject go;
    protected Transform transform;
  
    public YBaseState(GameObject gameObject)
    {
        this.go = gameObject;
        this.transform = gameObject.transform;
    }
    public abstract Type Tick();

}
