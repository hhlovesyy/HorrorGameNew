using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YMoveCamera : MonoBehaviour
{
    //�˴��빦�ܣ������λ����Զλ�ڽ�ɫ�۾�����

    public Transform cameraPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPos.position;
    }
}
