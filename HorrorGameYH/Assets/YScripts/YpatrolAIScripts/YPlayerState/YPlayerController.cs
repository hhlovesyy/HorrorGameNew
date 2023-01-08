using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPlayerController : MonoBehaviour
{
    public YStateMachine stateMachine => GetComponent<YStateMachine>();
    
    public void Awake()
    {
        InitStateMachine();
    }

    void InitStateMachine()
    {
        var states = new Dictionary<Type, YBaseState>
        {
            {typeof(YPlayerRunState),new YPlayerRunState(this) },
            {typeof(YPlayerBeHitState),new YPlayerBeHitState(this) }
        };
        GetComponent<YStateMachine>().SetStates(states);
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
