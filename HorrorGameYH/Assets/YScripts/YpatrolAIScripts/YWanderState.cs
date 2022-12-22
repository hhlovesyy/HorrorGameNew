using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YWanderState : YBaseState
{
    private Vector3? mDestination;
    public YPatrolAI partrolAI;
    public float stopDis;
    //构造函数 同时:base(yPatrolAI.gameObject) 是必要的 给父类传输形参yPatrolAI.gameObject
    public YWanderState(YPatrolAI yPatrolAI) : base(yPatrolAI.gameObject)
    {
        partrolAI = yPatrolAI;
    }
    public override Type Tick()
    {
        var chaseTarget = CheckForArrgo();
        if(chaseTarget)
        {
            partrolAI.setTarget(chaseTarget);
            return typeof(YChaseState);
        }
        if(mDestination.HasValue==false||
            Vector3.Distance(transform.position,mDestination.Value)<=stopDis)
        {
            FindRandomDis();
        }
        return null;
    }

    private void FindRandomDis()
    {
        throw new NotImplementedException();
    }

    private Transform CheckForArrgo()
    {
        return null;
    }
}
