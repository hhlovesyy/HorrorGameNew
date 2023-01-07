
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class YMoveCameraThirdPer: MonoBehaviour {
    private GameObject target;//声明注视目标
    public float distance = 3;//摄像机和目标的直线距离
    //横向旋转所需变量
    public float rot = 0;//横向角度
    public float rotSpeed = 100f;//横向旋转速度
    //纵向旋转使用的变量
    private float roll = 30f * Mathf.PI * 2 / 360;//纵向角度
    private float maxRoll = 70f * Mathf.PI * 2 / 360;//纵向最大角度
    private float minRoll = -10f * Mathf.PI * 2 / 360;//纵向最小角度
    public float rollSpeed = 20f;//纵向旋转速度
    //缩放视角使用的变量
    public float maxDistane = 10f;//鼠标滚轮最远距离
    public float minDistance = 1f;//最近距离
    public float scalSpeed = 0.4f;//距离变化速度
    //[ orientation方向] 这个放相机应该面向的方向
    public Transform orientation;
	void Start () {
        target = GameObject.Find("Player");//获得注视目标
	}
    void LateUpdate()
    {
        //检查注视目标和摄像机对象是否为空
        if (target == null)
            return;
        if (Camera.main == null)
            return;
        //执行横向旋转、纵向旋转和缩放视角方法
        Rotate();
        Roll();
        ScalCamera();
        Vector3 targetPos = target.transform.position;//获得目标坐标
        Vector3 cameraPos;//声明摄像机坐标
        float dx = distance * Mathf.Cos(roll);//获得摄像机和目标的水平距离
        float height = distance * Mathf.Sin(roll);//获得摄像机和目标的竖直距离
        cameraPos.x = targetPos.x + dx * Mathf.Cos(rot);//获得水平移动后的x坐标
        cameraPos.z = targetPos.z + dx * Mathf.Sin(rot);//获得水平移动后的z坐标
        cameraPos.y = targetPos.y + height;//获得相机y坐标
        Camera.main.transform.position = cameraPos;//确定摄像机坐标
        Camera.main.transform.LookAt(target.transform);//注视目标
        
        //orientation.transform.rotation = Quaternion.Euler(0,rot,0);
        orientation.transform.forward = Camera.main.transform.forward;
        orientation.transform.right = Camera.main.transform.right;
    }
    void Rotate()
    {
        float angleChange = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;//鼠标在X轴上移动的距离乘以旋转系数得到的旋转角度
        rot -= angleChange;//更新横向角度
        // if (Input.GetMouseButton(1)) //点击右键
        // {
        //     
        // }
    }
    void Roll()
    {
        float angleChange = Input.GetAxis("Mouse Y") * rollSpeed * 0.5f * Time.deltaTime;//鼠标在Y轴上移动的距离乘以旋转系数得到的旋转角度
        roll -= angleChange;//更新纵向角度
        if (roll > maxRoll)//纵向旋转的最大限制
            roll = maxRoll;
        if (roll < minRoll)//纵向旋转的最小限制
            roll = minRoll;
    }
    void ScalCamera()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) //鼠标中键向上滑轮
        {
            if (distance > minDistance)
                distance -= scalSpeed;//减少直线距离
        }
        else if (Input.GetAxis("Mouse ScrollWheel") <0)
        {
            if (distance < maxDistane)
                distance += scalSpeed;
        }
    }
   
}
