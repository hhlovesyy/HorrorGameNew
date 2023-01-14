using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPlayerProp : MonoBehaviour
{
    public static YPlayerProp instance;

    public int exp;
    public int gold;
    public int yuanshi;
    public List<YQuest> questList=new List<YQuest>();
    //后面也可以改为 
    //public Dictionary<string,YQuest> questDic=new Dictionary<string,YQuest>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
