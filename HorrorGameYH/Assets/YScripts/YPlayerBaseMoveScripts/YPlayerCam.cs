using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPlayerCam : MonoBehaviour
{
    //������   
    public float sensX;
    public float sensY;

    //[ orientation����] ����Ž�ɫ
    public Transform orientation;
    //public GameObject playerGo;
    float xRotation;
    float yRotation;

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

    // Update is called once per frame
    void Update()
    {
        //��ȡ�������
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //������Ҷ����������y��ת
        yRotation += mouseX;
        //������¶����������x��ת
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation,-90f,90f);

        //��ת���
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        //ͬʱ��ת��ɫ
        orientation.transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        //playerGo.transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
    }
}
