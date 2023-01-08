using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPlayerRunState : YBaseState
{
    private YPlayerController playerController;
    public YPlayerRunState(YPlayerController go) : base(go.gameObject)
    {
        playerController = go;
    }
    
    public override Type Tick()
    {
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
