using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum ElementBaseType
{
    None,
    Pyro,  //火
    Geo,  //岩
    Dendro, //草
    Anemo, //风
    Hydro, //水
    Cryo, //冰
    Electro, //雷
}

public enum AttackType
{
    None,
    ElementalBurst, //元素爆发
    ElementalSkill, //元素战技,
    NormalAttack, //普攻,
    ChargedAttack, //重击,
}

public class HEnumsUsedCommon
{
 
}
