using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class YWanderState : YBaseState
{
    private Vector3? mDestination;
    public YPatrolAI partrolAI;
    public float stopDis=1f;

    public Quaternion desiredRotation;
    public float turnSpeed=1f;
    public float rayDis=15f;
    private Vector3 mDirection;
    //private readonly LayerMask mlayerMask = LayerMask.NameToLayer("YWalls");
    private readonly LayerMask mlayerMask;
    private LayerMask wallLayer;
    //���캯�� ͬʱ:base(yPatrolAI.gameObject) �Ǳ�Ҫ�� �����ഫ���β�yPatrolAI.gameObject
    public YWanderState(YPatrolAI yPatrolAI) : base(yPatrolAI.gameObject)
    {
        partrolAI = yPatrolAI;
        wallLayer=partrolAI.mlayer;
        mlayerMask = partrolAI.mlayer;
    }
    public override Type Tick()
    {      
        var chaseTarget = CheckForAggro();
        if(chaseTarget)
        {
            partrolAI.setTarget(chaseTarget);
            partrolAI.animator.SetInteger("AnimState", 1);
            if (partrolAI.SpotLightWander) partrolAI.SpotLightWander.SetActive(false);
            if (partrolAI.SpotLightChase) partrolAI.SpotLightChase.SetActive(true);
            return typeof(YChaseState);
        }
        if(mDestination.HasValue==false||
            Vector3.Distance(transform.position,mDestination.Value)<=stopDis)
        {
            FindRandomDis();
        }

        //ab֮������ֵ
        transform.rotation = Quaternion.Slerp(transform.rotation,desiredRotation,Time.deltaTime*turnSpeed);

        //�����ɫ��ǰ��ǽ�� ��תһ�� ��ȡ��ײǽ
        if(IsForwardBlock())
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.2f);
        }
        else
        {
            transform.Translate(Vector3.forward*Time.deltaTime*YGameSetting.PatrolAISpeed);
        }
        Debug.DrawRay(transform.position,mDirection*rayDis,Color.red);

        //���ǰ���ķ�������ǽ��
        while(IsPathBlock())
        {
            FindRandomDis();
            Debug.Log("Wall!!");
        }

        return null;
    }

    private bool IsPathBlock()
    {
        Ray ray = new Ray(transform.position, mDirection);

        Debug.Log(LayerMask.LayerToName(mlayerMask));

        //������ɨ�����κ���ײ���ཻʱΪtrue������Ϊfalse��0.5f����뾶
        return Physics.SphereCast(ray, 0.5f, rayDis, mlayerMask);
    }

    private bool IsForwardBlock()
    {
        //Debug.DrawRay(transform.position,  transform.forward * rayDis, Color.blue);
        //���߼��
        Ray ray = new Ray(transform.position,transform.forward);
        //������ɨ�����κ���ײ���ཻʱΪtrue������Ϊfalse��0.5f����뾶
        //return Physics.SphereCast(ray,0.5f,rayDis,mlayerMask);
        bool res= Physics.SphereCast(ray, 0.5f, rayDis, mlayerMask);
        return res;
    }

    //private bool IsPathBlocked3()
    //{
    //    Debug.DrawRay(transform.position, mDirection * rayDis, Color.blue);
    //    Ray ray = new Ray(transform.position, mDirection);
    //    var hitSomething = Physics.RaycastAll(ray, rayDis, partrolAI.mlayer);
    //    return hitSomething.Any();
    //}
    private void FindRandomDis()
    {
        //���
        Vector3 testPos = (transform.position + (transform.forward * 4f)) +
            (new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, UnityEngine.Random.Range(-4.5f, 4.5f)));
        //Ŀ��
        mDestination = new Vector3(testPos.x, 1f, testPos.z);
        //����  (.Value)
        mDirection = Vector3.Normalize(mDestination.Value - transform.position);
        mDirection = new Vector3(mDirection.x,0f,mDirection.z);

        //��ת
        desiredRotation = Quaternion.LookRotation(mDirection);

        Debug.Log("Got Direction");
    }

    //Ѱ��Ŀ��
    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    //Ѱ��Ŀ��
    private Transform CheckForAggro()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit,YGameSetting.AggroRadius))
            {
                //var otherPartrolAI = hit.collider.GetComponent<YPatrolAI>();
                var aTarget = hit.collider.GetComponent<YPlayerMovement>();
                var aTargetLayer = hit.collider.gameObject;

                //if (aTarget != null && aTarget.team != gameObject.GetComponent<YPatrolAI>().team)
                // if (aTarget != null)
                // {
                //     Debug.DrawRay(pos, direction * hit.distance, Color.red);
                //     return aTarget.transform;
                // }
                // else if (aTargetLayer.layer.ToString() == "Yplayer")
                // {
                //     Debug.DrawRay(pos, direction * hit.distance, Color.blue);
                //     return aTargetLayer.transform;
                // }
                if(aTargetLayer.tag=="Player")
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.blue);
                    return aTargetLayer.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction * YGameSetting.AggroRadius, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }
}

