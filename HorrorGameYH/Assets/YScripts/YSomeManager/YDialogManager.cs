using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class YDialogManager : MonoBehaviour
{
    //单例模式
    public static YDialogManager instance;
    
    public GameObject dialogBox;

    public Text dialogText, nameText,showNameEnterText;
    [TextArea(1,3)] //为了在面板中可以显示为3行 不然一行可能装不下
    public string[] dialogLines;
    [SerializeField]private int curIndex;
    public GameObject playerGo;
    public bool isScrolling;
    [SerializeField]private float texSpeed;

    public float placeTextStep;
    public float originx;
    public GameObject showTalkEnter;
    public string[] rows;

    public int dialogAssetIndex;

    public string curLine;

    //使用两种方式来做对话 是否是textasset  如果是
    [Header("使用两种方式来做对话 是否是textasset  如果是,则使用放入的csv，lines忽略" +
            "如果不是 csv不用放 使用的是放入lines的对话")]
    public bool isTextAsset;

    //选项按钮预制体
    public GameObject optButtonPre;
    //选项父节点，用于自动排列
    public Transform btnGroup;

    public bool cursorOut;

    [Header("QuestAbout")]
    public YQuestable CurQuestable;//当前正在说话的对象 有什么任务

    public YQuestTarget questTarget;
    public YTalk talkable;
    
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //dialogText.text = dialogLines[curIndex];
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showTalkEnter.activeInHierarchy)
        {
            //showNameEnterText.text = tempName;
            
        }
        //如果他此时是激活状态
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (isScrolling == false && cursorOut == false) 
                {
                    if(!isTextAsset)
                    {
                        getTextNormal();
                    }
                    else
                    {
                        getMsgFromTextAsset();
                    }
                }
            }
        }
    }

    private void getTextNormal()
    {
        curIndex++;
        if (curIndex < dialogLines.Length)
        {
            curLine = dialogLines[curIndex];
            checkName();
            //dialogText.text = dialogLines[curIndex];
            float xtemp = originx - curLine.Length * placeTextStep;
            dialogText.transform.localPosition = new Vector3(xtemp,dialogText.transform.localPosition.y,dialogText.transform.localPosition.z);
            StartCoroutine(ScrollingText(curLine));
        }
        else
        {
            dialogBox.SetActive(false);
            playerGo.GetComponent<YPlayerMovement>()
                .canMove = true;
            //FindObjectOfType<YPlayerMovement>().canMove = true;
            
            // YCameraManager.instance.DialogCamera.gameObject.SetActive(false);
            YCameraManager.instance.changeDialogCamera(null,false);
            
            UpdateQuestAbout();
            
        }
    }
    public void showDialog(string[] lines)
    {
        dialogLines = lines;
        curIndex = 0;
        curLine = dialogLines[curIndex];
        checkName();
        curLine = dialogLines[curIndex];
        
        float xtemp = originx - curLine.Length * placeTextStep;
        dialogText.transform.localPosition = new Vector3(xtemp,dialogText.transform.localPosition.y,dialogText.transform.localPosition.z);
        
        //dialogText.text = dialogLines[curIndex];//一行一行读进来
        StartCoroutine(ScrollingText(curLine)); //本有
        //StartCoroutine(ScrollingAssetText());
        dialogBox.SetActive(true);
        
        //FindObjectOfType<YPlayerMovement>().canMove = false;
        playerGo.GetComponent<YPlayerMovement>()
            .canMove = false;
    }
    public void showAssetDialog(string Aline,string Aname)
    {
        float xtemp = originx - Aline.Length * placeTextStep;
        dialogText.transform.localPosition = new Vector3(xtemp,dialogText.transform.localPosition.y,dialogText.transform.localPosition.z);
        
        //dialogText.text = dialogLines[curIndex];//一行一行读进来
        StartCoroutine(ScrollingText(Aline)); //本有
        //StartCoroutine(ScrollingAssetText());
        dialogBox.SetActive(true);
        
        //FindObjectOfType<YPlayerMovement>().canMove = false;
        playerGo.GetComponent<YPlayerMovement>()
            .canMove = false;
        nameText.text = Aname;
    }
    private void checkName()
    {
        //如果以n-开头
        if (curLine.StartsWith("n-"))
        {
            nameText.text = curLine.Replace("n-","");
            curIndex++;
            curLine = dialogLines[curIndex];
        }
        
    }

    private IEnumerator ScrollingText(string cLine)
    {
        isScrolling = true;
        dialogText.text = "";

        foreach (char letter in cLine.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(texSpeed);
        }

        isScrolling = false;
    }

    public void showName(string name)
    {
        showNameEnterText.text = name;
    }

    public void readText(TextAsset dialogTextAsset)
    {
        playerGo.GetComponent<YPlayerMovement>()
            .canMove = true;
        //每行被“换行”分开       
        rows = dialogTextAsset.text.Split('\n');
        getMsgFromTextAsset();
    }

    public void getMsgFromTextAsset()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            //每个字母 被“,”分开
            string[] cell = rows[i].Split(',');
            if (cell[0] == "#" && int.Parse(cell[1]) == dialogAssetIndex)
            {
                showAssetDialog(cell[4],cell[2]);
                
                dialogAssetIndex = int.Parse(cell[5]);
                break;
            }
            else if (cell[0] == "&" && int.Parse(cell[1]) == dialogAssetIndex)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                GenerateButton(i);
                cursorOut = true;
            }
            else if (cell[0] == "END" && int.Parse(cell[1]) == dialogAssetIndex)
            {
                dialogBox.SetActive(false);
                playerGo.GetComponent<YPlayerMovement>()
                    .canMove = true;
                dialogAssetIndex = 0;

                // YCameraManager.instance.DialogCamera.gameObject.SetActive(false);
                YCameraManager.instance.changeDialogCamera(null, false);
            }
            
            if (cell[0] == "TriggerQuest" && int.Parse(cell[1]) == dialogAssetIndex)
            {
                Debug.Log("cell[0]"+cell[0]);
                //接下任务
                UpdateQuestAbout();
                
                //end操作
                dialogBox.SetActive(false);
                playerGo.GetComponent<YPlayerMovement>()
                    .canMove = true;
                dialogAssetIndex = 0;
                
                YCameraManager.instance.changeDialogCamera(null, false);
            }

            //questAbout
            // if (cell[8] !=""&& int.Parse(cell[1]) == dialogAssetIndex)
            // {
            //     Debug.Log("c"+cell[8]);
            // }

        }
    }

    public void GenerateButton(int BtnIndex)
    {
        string[] cell = rows[BtnIndex].Split(',');
        //判断下一行是不是选项卡
        if (cell[0] == "&" )
        {
            //将btn预制体生成在我们这个btnGroup里面
            GameObject btnGo = Instantiate(optButtonPre,btnGroup);
            btnGo.GetComponentInChildren<Text>().text=cell[4];
            btnGo.GetComponent<Button>().onClick.AddListener(
                delegate
            {
                onOptClick(int.Parse(cell[5]));
            });
            GenerateButton(BtnIndex + 1);
        }
        else
        {
            cursorOut = false;
        }
    }

    public void onOptClick(int id)
    {
        dialogAssetIndex = id;
        getMsgFromTextAsset();
        cursorOut = false;
        for (int i = 0; i < btnGroup.childCount; i++)
        {
            Destroy(btnGroup.GetChild(i).gameObject);
        }
    }

    public void UpdateQuestAbout()
    {
        if (CurQuestable == null)
        {
            //return;//有的NPC不会分配任务
            Debug.Log("NPC没有任务");
        }
        else
        {
            CurQuestable.DelegateQuest();//对话结束 发放委托
            YQuestManager.instance.updateQuestList();
        }
        
        //是某个任务完成需要接触的目标
        if (questTarget != null)
        {
            //我们希望 只有当接取任务之后才会去判断是否到达等
            for (int i = 0; i < YPlayerProp.instance.questList.Count; i++)
            {
                //任务已经被接取
                if (questTarget.questName == YPlayerProp.instance.questList[i].QuestName)
                {
                    questTarget.hasTalked = true;
                    questTarget.QuestCompleted();
                }
            }
        }
        else
        {
            return;
        }
        // Debug.Log(checkQuestIsCompleted());
        // if (checkQuestIsCompleted() && CurQuestable.isFinished == false) 
        // {
        //     showDialog(talkable.congratesLines);
        //     CurQuestable.isFinished = true;
        // }
        // else
        // {
        //    
        // }

    
    }

    public bool checkQuestIsCompleted()
    {
        //并未带有任务
        if (CurQuestable == null)
        {
            return false;
        }

        for (int i = 0; i < YPlayerProp.instance.questList.Count; i++)
        {
            if (CurQuestable.quest.QuestName == YPlayerProp.instance.questList[i].QuestName
                &&YPlayerProp.instance.questList[i].questStates==YQuest.QuestStates.Completed)
            {
                CurQuestable.quest.questStates = YQuest.QuestStates.Completed;
                return true;
            }
        }
        
        return false;
    }
}
