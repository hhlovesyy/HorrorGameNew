using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YQuestManager : MonoBehaviour
{
    public static YQuestManager instance;
    public GameObject[] questUIArr;
    
    public GameObject questPanel;
    public GameObject playerGo;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    //更新quest列表 当接到新的任务 或者任务完成过后 更新UI面板上的任务列表
    public void updateQuestList()
    {
        for (int i = 0; i < YPlayerProp.instance.questList.Count; i++)
        {
            //更新quest信息到UI上
            questUIArr[i].transform.GetChild(0).GetComponent<Text>().text = YPlayerProp.instance.questList[i].QuestName;
            if(YPlayerProp.instance.questList[i].questStates==YQuest.QuestStates.Accepted)
                questUIArr[i].transform.GetChild(1).GetComponent<Text>().text =
                    "<color=red>"+YPlayerProp.instance.questList[i].questStates.ToString()+"</color>";
            if(YPlayerProp.instance.questList[i].questStates==YQuest.QuestStates.Completed)
                questUIArr[i].transform.GetChild(1).GetComponent<Text>().text =
                    "<color=white>"+YPlayerProp.instance.questList[i].questStates.ToString()+"</color>";
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerGo.GetComponent<YPlayerMovement>()
                .canMove = questPanel.activeInHierarchy;
            questPanel.SetActive(!questPanel.activeInHierarchy);
        }
    }
}
