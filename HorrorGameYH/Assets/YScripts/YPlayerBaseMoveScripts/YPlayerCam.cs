using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPlayerCam : MonoBehaviour
{
    //灵敏度   
    public float sensX;
    public float sensY;

    //[ orientation方向] 这个放相机应该面向的方向
    public Transform orientation;
    //public GameObject playerGo;
    float xRotation;
    float yRotation;

    public float MX;
    public float MY;
    
    public GameObject C3D;
    // Start is called before the first frame update
    void Start()
    {
        //锁定鼠标
        //锁定时，光标位于视图的中心且无法移动。其实无论 Cursor.visible 的值如何，光标在此状态下均不可见。
        Cursor.lockState = CursorLockMode.Locked;
        //鼠标不可见
        //Cursor.visible = false;

        sensX = 400;
        sensY = 400;
    }
    //每一次setactive true都会再调用一次
    private void OnEnable()
    {
        //后面可以改为使用flag 第一次转换时做四元数的赋值 不转为vector3
        transform.rotation = orientation.transform.rotation;
        var temprot = orientation.transform.rotation.eulerAngles;
        xRotation = temprot.x;
        yRotation = temprot.y;
    }

    // Update is called once per frame
    void Update()
    {
        // MX = Input.GetAxisRaw("Mouse X");
        // MY = Input.GetAxisRaw("Mouse Y");
        // print(MX.ToString()+"  "+MY.ToString());
        //获取鼠标输入 Raw..
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        // float mouseX = MX * Time.deltaTime * sensX;
        // float mouseY = MY * Time.deltaTime * sensY;

        //鼠标左右动，相机绕着y轴转
        yRotation += mouseX;
        //鼠标上下动，相机绕着x轴转
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation,-90f,90f);

        //旋转相机
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        //同时旋转orientation
        orientation.transform.rotation = Quaternion.Euler(0,yRotation,0);
        //playerGo.transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
    }
}
