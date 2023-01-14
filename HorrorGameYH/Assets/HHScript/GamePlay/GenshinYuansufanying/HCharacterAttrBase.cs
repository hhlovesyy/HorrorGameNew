using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HCharacterAttrBase : MonoBehaviour
{
    //todo:基础属性暂时可以在面板调整,后面可能会改掉
    // 基础属性-白值部分
    [SerializeField]
    private float healthUpperLimitBase;
    [SerializeField]
    private float attackBase;
    private float defenseBase;
    private int elementalMasteryBase;
    private int maxStaminaBase;  //体力值上限

    //基础属性-绿值部分
    private float addHealth;
    private float addAttack;
    private float addDefense;
    private int addElementalMastery;
    private int addStamina;
    
    //圣遗物数组:todo:暂时设置为public,后面改为private
    public HArtifactAttr[] artifacts = new HArtifactAttr[5];
    public HWeaponAttr weapon;
    
    
    /// <summary>
    /// 判定角色死亡
    /// </summary>
    /// <param name="totalHealth"></param>
    private void CharacterDie(float totalHealth)
    {
        if (totalHealth <= 0.0f)
        {
            //todo:make character Die
            Debug.Log("血量值清零,角色死亡");
        }
    }
    
    //计算角色所佩戴的武器给角色带来的各个属性收益
    private void CalculateWeaponAdd(HWeaponAttr weapon)
    {
        //武器白值加到基础攻击里
        attackBase += weapon.WeaponAttackBase;
        addHealth += (weapon.WeaponHealthPercentageAdd / 100.0f * healthUpperLimitBase);  //武器的生命值加成加到绿值
        addDefense += (weapon.WeaponDenfensePercentageAdd / 100.0f * defenseBase);  //武器的防御力加成加到绿值
    }

    //计算角色所有的圣遗物和武器的属性面板对角色各个属性(包括白值和绿值的影响)
    private void CalculateArtifactsAndWeaponAddAttr(HArtifactAttr[] artifactAttrs, HWeaponAttr weapon)
    {
        CalculateWeaponAdd(weapon);
        
        for (int i = 0; i < artifactAttrs.Length; i++)
        {
            //计算绿值的生命部分 todo:暂时生命白值和防御的变化还没考虑,不知道什么会改这个白值
            CalulateAddHealth(artifactAttrs[i]);
            CalculateAddAttack(artifactAttrs[i]);
            CalculateAddDefense(artifactAttrs[i]);
        }
        ConvertFloatValueToInt();
    }

    private void CalculateAddDefense(HArtifactAttr artifact)
    {
        addDefense += (artifact.DefensePercentageAdd / 100.0f * defenseBase);
        addDefense += (artifact.DefenseValueAdd);
    }

    //计算绿值生命部分,包括其他来源的生命增益,如武器,圣遗物等
    private void CalulateAddHealth(HArtifactAttr artifact)
    {
        //百分比加成均以基础数值为准,固定加成直接体现在绿字上
        addHealth += (artifact.HealthPercentageAdd / 100.0f * healthUpperLimitBase);
        addHealth += (artifact.HealthValueAdd);
    }
    
    //计算绿值武器加成部分,所有百分比攻击加成都以基础数值为准,包括攻击沙,讨龙特效,双火共鸣等,
    //基础攻击力由角色属性和武器面板相加得到.
    private void CalculateAddAttack(HArtifactAttr artifact)
    {
        addAttack += (artifact.AttackPercentageAdd / 100.0f * attackBase);
        addAttack += (artifact.AttackValueAdd);
    }

    //将计算出的浮点数转换为整数,方便后面在UI上显示.todo:后面看看怎么更好更新这个数值
    private void ConvertFloatValueToInt()
    {
        healthUpperLimitBase = Mathf.Round(healthUpperLimitBase);
        addHealth = Mathf.Round(addHealth);
        attackBase = Mathf.Round(attackBase);
        addAttack = Mathf.Round(addAttack);
        
        print("greenHealthValue=");
        print(addHealth);
        print("BaseHealthValue=");
        print(healthUpperLimitBase);
        
        print("AttackBaseValue=");
        print(attackBase);
        print("AddAttackValue=");
        print(addAttack);
        
        print("DenfenseBaseValue=");
        print(defenseBase);
        print("AddDefenseValue=");
        print(addDefense);
    }

    //将所有的属性部分清零,todo:可能用新的初始化方式
    private void ClearAllValues()
    {
        addHealth = 0;
    }
    
    void Start()
    {
        ClearAllValues();
        
        
        //healthUpperLimitBase = 5013f;
        // HArtifactAttr artifact1 = new HArtifactAttr();
        // artifact1.HealthPercentageAdd = 46.6f;
        // artifact1.HealthValueAdd = 0;
        // HArtifactAttr[] artifacts = { artifact1 };

        CalculateArtifactsAndWeaponAddAttr(artifacts,weapon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //角色受到伤害的基本数值和怪物信息
    private void beHurtedValue()
    {
        
    }
}

