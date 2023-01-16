using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
// 基础的圣遗物属性
public class HArtifactAttr
{
    //以下属性均记录为浮点数值,如38.8,不包含百分号,todo:目前面板可见,方便调试,后面改成不可见
    [SerializeField] private float _healthPercentageAdd;
    [SerializeField] private int _healthValueAdd;

    [SerializeField] private float _attackPercentageAdd;
    [SerializeField] private int _attackValueAdd;

    [SerializeField] private float _defensePercentageAdd;
    [SerializeField] private int _defenseValueAdd;


    public HArtifactAttr()
    {
        
        
    }
    //构造函数,传入Dict,表示这个圣遗物的所有词条
    public HArtifactAttr(Dictionary<string,float> dict)
    {
        
    }
    public float DefensePercentageAdd
    {
        set { _defensePercentageAdd = value; }
        get { return _defensePercentageAdd; }
    }

    public int DefenseValueAdd
    {
        set { _defenseValueAdd = value; }
        get { return _defenseValueAdd; }
    }


    public float HealthPercentageAdd
    {
        //todo:暂时可以set,后面应该要去掉,下同:
        set { _healthPercentageAdd = value;}
        get { return _healthPercentageAdd; }
    }

    public int HealthValueAdd
    {
        set { _healthValueAdd = value;}
        get { return _healthValueAdd; }
    }
    
    public float AttackPercentageAdd
    {
        set { _attackPercentageAdd = value; }
        get { return _attackPercentageAdd; }
    }

    public int AttackValueAdd
    {
        set { _attackValueAdd = value; }
        get { return _attackValueAdd; }
    }


}