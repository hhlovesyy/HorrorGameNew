using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPlayerIdleState : YBaseState
{
    private YPlayerController playerController;
    public YPlayerIdleState(YPlayerController go):base(go.gameObject)
    {
        playerController = go;
    }

    public override Type Tick()
    {
        Debug.Log("idle");
        // myInput();
        // SpeedControl();
        playerController.rb.drag = playerController.groundDrag;
        
        
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
