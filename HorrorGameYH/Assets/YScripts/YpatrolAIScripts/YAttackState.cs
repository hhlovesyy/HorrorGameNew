using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class YAttackState : YBaseState
{
    private YPatrolAI patrolAI;
    private float tempAttackReadyTimer;
    public YAttackState(YPatrolAI yPatrolAI):base(yPatrolAI.gameObject)
    {
        patrolAI = yPatrolAI;
        tempAttackReadyTimer = 0f;
    }
    public override Type Tick()
    {
        if(patrolAI.mTarget==null)
        {
            return typeof(YWanderState);
        }
        //Debug.Log("Atack");
        //patrolAI.AttackFunc();
        //patrolAI.animator.SetInteger("AnimState", 1);
        //return typeof(YChaseState);

        //���ܲ������� �����Ļ��Ϳ��ܻ���ֹ�����һ֡ 

        tempAttackReadyTimer -= Time.deltaTime;
        if (tempAttackReadyTimer <= 0f)
        {
            Debug.Log("Atack");
            //YUIManager.flashScreen();

            //attack��ʱ�����Ž�ɫ
            transform.LookAt(patrolAI.mTarget);
            patrolAI.AttackFunc();
            tempAttackReadyTimer = YGameSetting.attackReadyTimer;
            //�����������
            //��Ŀ������Լ��Ѿ�����������Χ �˴� ��׷��Χ�ںͲ��ڶ���ʱֱ����������wanderStater
            if (Vector3.Distance(transform.position, patrolAI.mTarget.transform.position) > YGameSetting.AttackRange)
            {
                patrolAI.animator.SetInteger("AnimState", 1);
                return typeof(YChaseState);
            }
        }
        // �����������
        // ��Ŀ������Լ��Ѿ�����������Χ �˴� ��׷��Χ�ںͲ��ڶ���ʱֱ����������wanderStater
        //if (Vector3.Distance(transform.position, patrolAI.mTarget.transform.position) > YGameSetting.AttackRange)
        //{
        //    return typeof(YWanderState);
        //}

        //// �����������
        //// ��Ŀ������Լ��Ѿ�����������Χ �˴� ��׷��Χ�ںͲ��ڶ���ʱֱ����������wanderStater
        //if (tempAttackReadyTimer <= 0f
        //    && Vector3.Distance(transform.position, patrolAI.mTarget.transform.position) > YGameSetting.AttackRange)
        //{
        //    tempAttackReadyTimer = YGameSetting.attackReadyTimer;
        //    return typeof(YWanderState);
        //}
        return null;
    }

}
