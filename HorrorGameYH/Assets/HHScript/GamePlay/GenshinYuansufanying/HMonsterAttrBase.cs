using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HMonsterAttrBase : MonoBehaviour
{
    [SerializeField]
    private int _monsterLevel;

    [SerializeField] private ElementBaseType element;

    public int MonsterLevel
    {
        set { _monsterLevel = value; }
        get { return _monsterLevel; }
    }

    //对目标(常见角色)能够造成的"最终"伤害值(已经经过原魔的各种加成)
    public void HurtTarget(GameObject target, float value)
    {
        HCharacterAttrBase characterAttrBase = target.gameObject.GetComponentInChildren<HCharacterAttrBase>();
        if (!characterAttrBase)
        {
            Debug.LogError("target dont have characterAttrBase script!");
            return;
        }
        characterAttrBase.beHurtedValue(value,this,element);
        
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

