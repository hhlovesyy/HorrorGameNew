using UnityEngine;
//参考教程：【【任务系统/上】通过与NPC对话领取不同种类的任务，并且在UI任务列表更新显示（附：对话系统，单向平台设计，场景转换，单例模式）】
//https://www.bilibili.com/video/BV1ut4y1973c/?share_source=copy_web&vd_source=067de257d5f13e60e5b36da1a0ec151e
[System.Serializable]
//以上这句保证在unity中拥有这个类的时候 这个类的信息被显示
public class YQuest
{
    public enum QuestType
    {
        Gathering,
        Talk,
        Reach
    };
    public enum QuestStates
    {
        Waitting,
        Accepted,
        Completed,
    }

    public string QuestName;
    public QuestType questType;
    public QuestStates questStates;

    public int ExpRewards;
    public int goldRewards;
    public int yuanshiRewards;

    [Header("搜集任务")] public int requireAmount;

}
