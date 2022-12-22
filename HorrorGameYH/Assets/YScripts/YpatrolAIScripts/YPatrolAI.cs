using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPatrolAI : MonoBehaviour
{
    [SerializeField] private Team mTeam;
    //只是一个光束
    [SerializeField] private GameObject laserVisual;

    //我们希望能够读取目标 但是不能设置目标 比如敌方机器可以读取我们的位置但是不能设置我们的位置等
    public Transform target { get; private set; }

    //以下这句话就是返回_team 表示一个匿名函数 其实也是public Team team；的意思 只是以下防止我们去修改我们的_team
    //lambda表达式 传入team 传出mTeam
    public Team team => mTeam;
    public YStateMachine stateMachine => GetComponent<YStateMachine>();

    private void Awake()
    {
        InitStateMachine();
    }

    private void InitStateMachine()
    {
        var states = new Dictionary<Type, YBaseState>
        {
            {typeof(YWanderState),new YWanderState(this) }
        };
        GetComponent<YStateMachine>().SetStates(states);
    }
    
    public void setTarget(Transform tar)
    {
        target = tar;
    }
    public enum Team
    {
        Enemy,
        Friend
    }
}
