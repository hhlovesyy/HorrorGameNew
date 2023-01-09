using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPlayerCam : MonoBehaviour
{
    //������   
    public float sensX;
    public float sensY;

    //[ orientation����] ��������Ӧ������ķ���
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
        //�������
        //����ʱ�����λ����ͼ���������޷��ƶ�����ʵ���� Cursor.visible ��ֵ��Σ�����ڴ�״̬�¾����ɼ���
        Cursor.lockState = CursorLockMode.Locked;
        //��겻�ɼ�
        //Cursor.visible = false;

        sensX = 400;
        sensY = 400;
    }
    //ÿһ��setactive true�����ٵ���һ��
    private void OnEnable()
    {
        //������Ը�Ϊʹ��flag ��һ��ת��ʱ����Ԫ���ĸ�ֵ ��תΪvector3
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
        //��ȡ������� Raw..
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        // float mouseX = MX * Time.deltaTime * sensX;
        // float mouseY = MY * Time.deltaTime * sensY;

        //������Ҷ����������y��ת
        yRotation += mouseX;
        //������¶����������x��ת
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation,-90f,90f);

        //��ת���
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        //ͬʱ��תorientation
        orientation.transform.rotation = Quaternion.Euler(0,yRotation,0);
        //playerGo.transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
    }
}
