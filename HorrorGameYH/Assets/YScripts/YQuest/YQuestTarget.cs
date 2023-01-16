using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 任务目标所挂载的脚本，用于判断是否达成任务目的
/// </summary>
public class YQuestTarget : MonoBehaviour
{
    public string questName;
    public enum QuestType
    {
        Gathering,
        Talk,
        Reach
    };

    public QuestType questType;

    [Header("收集类型任务")] public int amount = 1;

    [Header("对话类型任务")] public bool hasTalked;

    [Header("探索类型任务")] public bool hasReached;
    /// <summary>
    /// 在任务完成之后调用 例如收集完所有东西，和需要对话的NPC对话完,或者到达所需要的区域
    /// </summary>
    public void QuestCompleted()
    {
        for (int i = 0; i < YPlayerProp.instance.questList.Count; i++)
        {
            //如果玩家任务列表里面有这个任务 && 这个任务是正在进行的接收状态
            if (questName == YPlayerProp.instance.questList[i].QuestName
                &&YPlayerProp.instance.questList[i].questStates==YQuest.QuestStates.Accepted)
            {
                switch (questType)
                {
                    case QuestType.Gathering:
                        //看看是否已经数量到达了
                        if (YPlayerProp.instance.itemAmount >= YPlayerProp.instance.questList[i].requireAmount)
                        {
                            YPlayerProp.instance.questList[i].questStates = YQuest.QuestStates.Completed;
                            YQuestManager.instance.updateQuestList();
                        }
                        break;
                    case QuestType.Talk:
                        if (hasTalked)
                        {
                            YPlayerProp.instance.questList[i].questStates = YQuest.QuestStates.Completed;
                            YQuestManager.instance.updateQuestList();
                        }
                        break;
                    case QuestType.Reach:
                        if (hasReached)
                        {
                            YPlayerProp.instance.questList[i].questStates = YQuest.QuestStates.Completed;
                            YQuestManager.instance.updateQuestList();
                        }
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //我们希望 只有当接取任务之后才会去判断是否到达等
            for (int i = 0; i < YPlayerProp.instance.questList.Count; i++)
            {
                //任务已经被接取
                if (questName==YPlayerProp.instance.questList[i].QuestName)
                {
                    if (questType == QuestType.Reach)
                    {
                        hasReached = true;
                        QuestCompleted();
                    }
                    if (questType == QuestType.Gathering)
                    {
                        //YPlayerProp.instance.itemAmount += 1;
                        QuestCompleted();
                    }
                }
            }
           
        }
    }
}
