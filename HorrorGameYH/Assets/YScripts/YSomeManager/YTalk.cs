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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = true;
            YDialogManager.instance.showName(name);
            YDialogManager.instance.showTalkEnter.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            YDialogManager.instance.showTalkEnter.SetActive(false);
            isEnter = false;
        }
    }

    void Update()
    {
        if (isEnter && Input.GetKeyDown(KeyCode.F)&& YDialogManager.instance.dialogBox.activeInHierarchy==false)
        {
            YDialogManager.instance.showTalkEnter.SetActive(false);
            YCameraManager.instance.changeDialogCamera(lookAtTrans,true);
            //YDialogManager.instance.changeDialogCamera(lookAtTrans);
            //camera 移动
            if (!isAsset)
            {
                YDialogManager.instance.showDialog(lines);
                YDialogManager.instance.isTextAsset = false;
            }
            else
            {
                YDialogManager.instance.readText(dialogTextAsset);
                YDialogManager.instance.isTextAsset = true;
            }
        }
    }
}
