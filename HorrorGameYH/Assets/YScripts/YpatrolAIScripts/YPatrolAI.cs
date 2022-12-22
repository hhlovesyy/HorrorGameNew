using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPatrolAI : MonoBehaviour
{
    [SerializeField] private Team mTeam;
    //ֻ��һ������
    [SerializeField] private GameObject laserVisual;

    //����ϣ���ܹ���ȡĿ�� ���ǲ�������Ŀ�� ����з��������Զ�ȡ���ǵ�λ�õ��ǲ����������ǵ�λ�õ�
    public Transform target { get; private set; }

    //������仰���Ƿ���_team ��ʾһ���������� ��ʵҲ��public Team team������˼ ֻ�����·�ֹ����ȥ�޸����ǵ�_team
    //lambda���ʽ ����team ����mTeam
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
