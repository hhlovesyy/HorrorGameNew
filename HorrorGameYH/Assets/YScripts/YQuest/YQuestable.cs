using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个脚本将会被添加在所有会“委派任务”的NPC/东西上
public class YQuestable : MonoBehaviour
{
    public YQuest quest;

    //此方法将会在 与可分配任务的NPC选择相应的对话完之后【调用】
    //分配任务
    public void DelegateQuest()
    {
        if (quest.questStates == YQuest.QuestStates.Waitting)
        {
            //可以领取这个任务
            YPlayerProp.instance.questList.Add(quest);
        }
        else 
        {
            //Debug.Log的另一写法
            Debug.Log(string.Format("已经领取了任务{0}",quest.QuestName));
            //任务列表中已经拥有这个任务了 不能重复领取任务
        }
    }
}
