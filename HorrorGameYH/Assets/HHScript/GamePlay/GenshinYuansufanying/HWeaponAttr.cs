using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HWeaponAttr
{
    [SerializeField]
    //以下属性均记录为浮点数值,如38.8,不包含百分号
    private float _weaponHealthPercentageAdd;

    [SerializeField] private int _weaponAttackBase; //武器白值
    [SerializeField] private float _weaponDefensePercentageAdd; //防御力加成

    public int WeaponAttackBase
    {
        get { return _weaponAttackBase;}
        set { _weaponAttackBase = value; }
    }

    public float WeaponHealthPercentageAdd
    {
        get { return _weaponHealthPercentageAdd; }
        set { _weaponHealthPercentageAdd = value; }
    }

    public float WeaponDenfensePercentageAdd
    {
        get { return _weaponDefensePercentageAdd; }
        set { _weaponDefensePercentageAdd = value; }
    }

}

