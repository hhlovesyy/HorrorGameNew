using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class YTalk : MonoBehaviour
{
    [SerializeField]private bool isEnter;
    [TextArea(1, 3)] 
    public string[] lines;

    public string name;
    public bool isAsset;
    public TextAsset dialogTextAsset;

    public Transform lookAtTrans;

    [Header("Quest")]
    private YQuestable Questable;//表明它有委派任务的能力

    public YQuestTarget QuestTarget;
    [TextArea(1, 4)] public string[] congratesLines;
    [TextArea(1, 4)] public string[] completedLines;
    
    public TextAsset dialogCongratesTextAsset;
    public TextAsset dialogDefaultTextAsset;
    public TextAsset dialogWaitForCompTextAsset;

    public bool isDialog;
    //public TextAsset defaultWordTextAsset;
    
    //是否是可以交互状态
    //public bool bInteractive;
    private void Start()
    {
        Questable = GetComponent<YQuestable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            YDialogManager.instance.CurQuestable = Questable;
            YDialogManager.instance.questTarget = QuestTarget;
            YDialogManager.instance.talkable = this;
            
            isEnter = true;
            // YDialogManager.instance.showName(name);
            // YDialogManager.instance.showTalkEnter.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            YDialogManager.instance.CurQuestable = null;
            YDialogManager.instance.showTalkEnter.SetActive(false);
            isEnter = false;
        }
    }

    void Update()
    {
        if (isEnter&& YDialogManager.instance.dialogBox.activeInHierarchy==false)
        {
            if (!updateInterationState())
            {
                return;
            }
            else
            {
                //以下暂时没实现 用对话后会出现......代替
                //有时候这个角色可以对话  有时候不能对话
                //我们可以存(这里是判断)一个是否能对话的状态，不能对话的话就给不让他出现那个F的标志
                //可以让他每次任务推进一步就update一下

                YDialogManager.instance.showName(name);
                YDialogManager.instance.showTalkEnter.SetActive(true);
            }
        }
        if (isEnter && Input.GetKeyDown(KeyCode.F)&& YDialogManager.instance.dialogBox.activeInHierarchy==false)
        {
            //isDialog = true;
            YDialogManager.instance.showTalkEnter.SetActive(false);
            YCameraManager.instance.changeDialogCamera(lookAtTrans,true);
            //YDialogManager.instance.changeDialogCamera(lookAtTrans);
            //camera 移动
            if (!isAsset)
            {
                YDialogManager.instance.isTextAsset = false;
                if (Questable == null)
                {
                    YDialogManager.instance.showDialog(lines);
                }
                else
                {
                    if (Questable.quest.questStates == YQuest.QuestStates.Waitting)
                    {
                        YDialogManager.instance.showDialog(lines);
                    }
                    else if(Questable.quest.questStates == YQuest.QuestStates.Accepted)
                    {
                        //YDialogManager.instance.showDialog(waitingQuestLines);
                        return;
                    }
                    else if (Questable.quest.questStates == YQuest.QuestStates.Completed
                             &&! Questable.isFinished)//YQuest.QuestStates.Completed
                    {
                        if (YDialogManager.instance.checkQuestIsCompleted())
                        {
                            YDialogManager.instance.showDialog(congratesLines);
                            Questable.isFinished = true;
                        }
                    }
                    else
                    {
                        YDialogManager.instance.showDialog(completedLines);
                    }
                }
            }
            else
            {
                YDialogManager.instance.isTextAsset = true;
                
                if (Questable == null)
                {
                    toShowdialogTextAsset(dialogTextAsset);
                }
                else
                {
                    if (Questable.quest.questStates == YQuest.QuestStates.Waitting)
                    {
                        toShowdialogTextAsset(dialogTextAsset);
                    }
                    else if(Questable.quest.questStates == YQuest.QuestStates.Accepted)
                    {
                        toShowdialogTextAsset(dialogWaitForCompTextAsset);
                    }
                    else if (Questable.quest.questStates == YQuest.QuestStates.Completed
                             &&! Questable.isFinished)//YQuest.QuestStates.Completed
                    {
                        if (YDialogManager.instance.checkQuestIsCompleted())
                        {
                            //读取完成任务的对话
                            toShowdialogTextAsset(dialogCongratesTextAsset);
                            Questable.isFinished = true;
                        }
                    }
                    else
                    {
                        toShowdialogTextAsset(dialogDefaultTextAsset);
                    }
                }
            }
            
        }
    }

    public void toShowdialogTextAsset(TextAsset textAsset)
    {
        if (textAsset)
        {
            YDialogManager.instance.readText(textAsset);
        }
    }
    public bool updateInterationState()
    {
        if (!isAsset)
        {
            YDialogManager.instance.isTextAsset = false;
            if (Questable == null)
            {
                if (lines == null) return false;
            }
            else
            {
                if (Questable.quest.questStates == YQuest.QuestStates.Waitting)
                {
                    if (lines == null) return false;
                }
                else if(Questable.quest.questStates == YQuest.QuestStates.Accepted)
                {
                    return false;
                }
                else if (Questable.quest.questStates == YQuest.QuestStates.Completed
                         &&! Questable.isFinished)//YQuest.QuestStates.Completed
                {
                    if (YDialogManager.instance.checkQuestIsCompleted())
                    {
                        if (congratesLines == null) return false;
                    }
                }
                else
                {
                    if (completedLines == null) return false;
                }
            }
        }
        else
        {
            YDialogManager.instance.isTextAsset = true;
            
            if (Questable == null)
            {
                if (dialogTextAsset == null) return false;
            }
            else
            {
                if (Questable.quest.questStates == YQuest.QuestStates.Waitting)
                {
                    if (dialogTextAsset == null) return false;
                }
                else if(Questable.quest.questStates == YQuest.QuestStates.Accepted)
                {
                    if (dialogWaitForCompTextAsset == null) return false;
                }
                else if (Questable.quest.questStates == YQuest.QuestStates.Completed
                         &&! Questable.isFinished)//YQuest.QuestStates.Completed
                {
                    if (YDialogManager.instance.checkQuestIsCompleted())
                    {
                        //读取完成任务的对话
                        if (dialogCongratesTextAsset == null)
                        {//有点怪 意思是说如果没有完成任务后的奖励对话，那么我们直接结束这个任务
                            Questable.isFinished = true;
                            //并且直接判断默认对话是都拥有
                            if (dialogDefaultTextAsset == null) return false;
                        }
                    }
                }
                else
                {
                    if (dialogDefaultTextAsset == null) return false;
                }
            }
        }
        return true;
    }
}
