using System;
using System.Collections;
using System.Collections.Generic;
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
    //public string tempName;
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
        dialogText.text = dialogLines[curIndex];
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
                if (isScrolling == false)
                {
                    curIndex++;
                    if (curIndex < dialogLines.Length)
                    {
                        checkName();
                        //dialogText.text = dialogLines[curIndex];
                        float xtemp = originx - dialogLines[curIndex].Length * placeTextStep;
                        dialogText.transform.localPosition = new Vector3(xtemp,dialogText.transform.localPosition.y,dialogText.transform.localPosition.z);
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        dialogBox.SetActive(false);
                        playerGo.GetComponent<YPlayerMovement>()
                            .canMove = true;
                        //FindObjectOfType<YPlayerMovement>().canMove = true;
                    }
                }
            }
        }
    }

    public void showDialog(string[] lines)
    {
        dialogLines = lines;
        curIndex = 0;
        checkName();
        
        float xtemp = originx - dialogLines[curIndex].Length * placeTextStep;
        dialogText.transform.localPosition = new Vector3(xtemp,dialogText.transform.localPosition.y,dialogText.transform.localPosition.z);
        
        //dialogText.text = dialogLines[curIndex];//一行一行读进来
        StartCoroutine(ScrollingText());
        dialogBox.SetActive(true);
        
        //FindObjectOfType<YPlayerMovement>().canMove = false;
        playerGo.GetComponent<YPlayerMovement>()
            .canMove = false;
    }

    private void checkName()
    {
        //如果以n-开头
        if (dialogLines[curIndex].StartsWith("n-"))
        {
            nameText.text = dialogLines[curIndex].Replace("n-","");
            curIndex++;
        }
        
    }

    private IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogText.text = "";

        foreach (char letter in dialogLines[curIndex].ToCharArray())
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
}
