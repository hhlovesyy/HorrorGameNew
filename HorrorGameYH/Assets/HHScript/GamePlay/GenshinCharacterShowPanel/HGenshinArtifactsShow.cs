using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XArrayPool = Unity.VisualScripting.XArrayPool;

//todo:根据圣遗物的索引定位到上次旋转的位置

[Serializable]
public class ArtifactPosStruct
{
    public Vector3 artifactPos;
    public Quaternion artifactRot;
    public Vector3 artifactScale;

    public ArtifactPosStruct(Vector3 pos,Quaternion rot, Vector3 scale)
    {
        artifactPos = pos;
        artifactRot = rot;
        artifactScale = scale;
    }

    public ArtifactPosStruct()
    {
    }
}

public class HGenshinArtifactsShow : MonoBehaviour
{
    //横向旋转所需变量
    //public float rot = 0;//横向角度
    public float rotSpeed = 1f;//横向旋转速度
    public Transform artifactsCenter;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    public Camera sceneCamera;

    [SerializeField] private Transform[] worldPos;
    [SerializeField] private RectTransform[] rectTransform;
    public int artifactNum = 5;
    public Vector3 levelOffset;
    private int pixelHeight;
    private int pixelWidth;
    private float cameraDistance;
    public float rotateIdleSpeed;

    private bool isCheckingArtifact;
    public Transform artifactDetailLocation;
    private Transform chooseArtifactPos;
    private int curChooseIndex;
    //public Transform[] artifactsPoses;
    public ArtifactPosStruct[] artifactPosStructs;

    void ArtifactsRotate()
    {
        if (isCheckingArtifact)
        {
             return;//如果正在检查圣遗物
        }
        
        artifactsCenter.Rotate(Vector3.up, -rotateIdleSpeed, Space.World);
        //鼠标拖动5个圣遗物旋转
        if (Input.GetMouseButton(0))
        {
            mPosDelta = Input.mousePosition - mPrevPos;
            artifactsCenter.Rotate(Vector3.up, -mPosDelta.x*rotSpeed, Space.World);
        }
        mPrevPos = Input.mousePosition;
        
        //圣遗物下面的等级要一直面向屏幕,并且不需要缩放,因此考虑用UI来完成,圣遗物的图片考虑用Texture来实现,但是通过控制旋转角度不变使其一直面向屏幕
        for (int i = 0; i < artifactNum; i++)
        {
            //处理UI旋转相关的逻辑,让UI位置与物体的世界空间位置对应
            Vector3 screenPos = sceneCamera.WorldToScreenPoint(worldPos[i].transform.position);
            rectTransform[i].position = screenPos + levelOffset;
            
            //可见性用screenpos的z来做运算会比较方便
            if(screenPos.z>=cameraDistance*1.2f) rectTransform[i].gameObject.SetActive(false);
            else rectTransform[i].gameObject.SetActive(true);
            
        }
        
        
        //todo:暂时采用magic做法来做UI的自动缩放,后面看看让鲁棒性变强一点.
        //rectTransform.localScale = new Vector3(worldPos.transform.position.x / 2.0f + 0.8f,worldPos.transform.position.x / 2.0f + 0.8f,1);
        //print(screenPos.z);
    }

    void CheckMouseClick()
    {
        //获得摄像头与鼠标位置方向射线的向量
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //判断是否点击左键&&射线是否碰到有碰撞器的东西
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray,out RaycastHit hitinfo))
        {
            // todo:如果在拖拽状态的时候不要进点击圣遗物的界面,不过暂时没有很好地实现思路,后面再说
            if (hitinfo.collider.gameObject.CompareTag("canMousePointTo"))
            {
                if (isCheckingArtifact) 
                     return;
                //如果点击圣遗物,进入详情界面
                
                for(int i=0;i<artifactNum;i++) rectTransform[i].gameObject.SetActive(false);

                print(hitinfo.collider.gameObject.name);
                 for (int i = 0; i < artifactNum; i++)
                 {
                     //print(worldPos[i].gameObject.name);
                     //点击某个圣遗物之后,其他的圣遗物可视化要去掉,并且被点到的圣遗物要放大往上
                     if (hitinfo.collider.gameObject.name != worldPos[i].gameObject.name)
                     {
                         worldPos[i].gameObject.SetActive(false);
                     }
                     else
                     {
                          worldPos[i].gameObject.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
                          worldPos[i].gameObject.transform.position = artifactDetailLocation.position;
                     }
                 }
                isCheckingArtifact = true;
                
            }
        }
    }

    public void ShowArtifactDetailWithCharacter()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        pixelHeight = sceneCamera.pixelHeight;
        pixelWidth = sceneCamera.pixelWidth;
        Vector3 screenPos = sceneCamera.WorldToScreenPoint(worldPos[0].transform.position);
        cameraDistance = screenPos.z;
        levelOffset = new Vector3(0f, -pixelHeight / 7.0f, 0f);
        
        isCheckingArtifact = false;
        artifactNum = 5;
        
        //坑点!!!!不能记录Transform,因为transform是引用类型,会随着位置动而发生改变,真阴间.
        artifactPosStructs = new ArtifactPosStruct[artifactNum];
        for (int i = 0; i < artifactNum; i++)
        {
            //请注意这是C#的特点,这里面也要new一下,不然会报错
            artifactPosStructs[i] =
                new ArtifactPosStruct(worldPos[i].position, worldPos[i].rotation, worldPos[i].localScale);
        }
        //print("llalala");
        //print(worldPos[0].gameObject.transform.position);
        ResetArtifacts();
    }

    void ResetArtifacts()
    {
        for (int i = 0; i < artifactNum; i++)
        {
            worldPos[i].gameObject.SetActive(true);
            //rectTransform[i].gameObject.SetActive(true);
            worldPos[i].gameObject.transform.position = artifactPosStructs[i].artifactPos;
            worldPos[i].gameObject.transform.rotation = artifactPosStructs[i].artifactRot;
            worldPos[i].gameObject.transform.localScale = artifactPosStructs[i].artifactScale;
        }
    }
    //退出当前界面的效果
    void DealWithExit()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ResetArtifacts();
            isCheckingArtifact = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ArtifactsRotate();
        CheckMouseClick();
        DealWithExit();

    }
}
