
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class YMoveCameraThirdPer: MonoBehaviour {
    //private GameObject target;//声明注视目标
    public GameObject target;
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
    public GameObject Camera3D;
    public float lockDistance;
    public int CameraState=0;//正在前进 正在后退 保持
    
    void Start ()
    {
        Camera3D = gameObject;
        lockDistance = distance;
    }
    private void OnEnable()
    {
        transform.rotation = orientation.transform.rotation;
        var temprot = orientation.transform.rotation.eulerAngles;
        roll = 0f ;//默认纵向角度
        rot = (-1)*(temprot.y*Mathf.PI * 1 / 180 +Mathf.PI/2f);
    }
    void LateUpdate()
    {
        //检查注视目标和摄像机对象是否为空
        if (target == null)
            return;
        // if (Camera.main == null)
        //     return;
        if (gameObject == null)
            return;

        //! 代表前面有墙壁 相机正在往前拉
        //代表没障碍 OK走的
        // bool isdf = detectFor();
        // if (isdf)
        // {
        //     bool isdt = detectBack();
        // }
        switch (CameraState)
        {
            case 0:
                if (detectFor())
                {
                    detectBack();
                }
                break;
            case 1:
                detectFor();
                break;
            case 2:
                detectBack();
                break;
        }

        //执行横向旋转、纵向旋转和缩放视角方法
        Rotate();
        Roll();
        //ScalCamera();
        
        Vector3 targetPos = target.transform.position;//获得目标坐标
        Vector3 cameraPos;//声明摄像机坐标
        float dx = distance * Mathf.Cos(roll);//获得摄像机和目标的水平距离
        float height = distance * Mathf.Sin(roll);//获得摄像机和目标的竖直距离
        cameraPos.x = targetPos.x + dx * Mathf.Cos(rot);//获得水平移动后的x坐标
        cameraPos.z = targetPos.z + dx * Mathf.Sin(rot);//获得水平移动后的z坐标
        cameraPos.y = targetPos.y + height;//获得相机y坐标
        // Camera.main.transform.position = cameraPos;//确定摄像机坐标
        // Camera.main.transform.LookAt(target.transform);//注视目标
        gameObject.transform.position = cameraPos;//确定摄像机坐标
        gameObject.transform.LookAt(target.transform);//注视目标
        
        // orientation.transform.forward = Camera.main.transform.forward;
        // orientation.transform.right = Camera.main.transform.right;
        orientation.transform.forward = gameObject.transform.forward;
        orientation.transform.right = gameObject.transform.right;
    }

    bool detectFor()
    {
        RaycastHit hitInfo;
        
        //Vector3 fwd = gameObject.transform.TransformDirection(Vector3.forward);
        //if (Physics.SphereCast(gameObject.transform.position,0.1f, gameObject.transform.forward, out hitInfo, distance))
        if (Physics.Raycast(gameObject.transform.position,gameObject.transform.forward, out hitInfo, distance))
        {
            string name = hitInfo.collider.gameObject.tag;
            if (name != "Player")
            {
                //如果射线碰撞的不是相机，那么就取得 相机到射线碰撞点 的距离
                float currentDistance = Vector3.Distance(transform.position,hitInfo.point);
                //如果射线碰撞点小于玩家与相机本来的距离，就说明角色身后是有东西，为了避免穿墙，就把相机拉近
                if (currentDistance < distance)
                {
                    //Debug.DrawLine(transform.position,transform.position+gameObject.transform.forward*distance ,Color.blue,distance);
                    
                    //m_transsform.position = hitInfo.point;
                    //Debug.Log("hit"+hitInfo.collider);
                    //Debug.Log(distance+"ddd");
                    //Vector3.Normalize(Camera.main.transform.TransformDirection(Vector3.back)) * dis * Time.deltaTime;
  
                    distance = Mathf.Lerp(distance,distance-currentDistance,Time.deltaTime*10f);
                    if (distance <= 0.65f) distance = 0.65f;
                    //Debug.Log(distance);
                    //Debug.DrawLine(transform.position,transform.position+gameObject.transform.forward*distance ,Color.red,distance);

                    CameraState = 1;//forward state
                    return false;
                }
            }
            CameraState = 0;
            return true;
        }
        CameraState = 0;
        return true;
    }

    bool detectBack()
    {
        RaycastHit hitInfo;
        //反方向退回去 但是需要注意的是 要退到墙上
        if (distance < lockDistance)
        {
            
            //if (Physics.SphereCast(gameObject.transform.position, 0.1f, gameObject.transform.forward * (-1),
            if (Physics.Raycast(gameObject.transform.position,  gameObject.transform.forward * (-1),
                    out hitInfo,
                    distance))
            {
                string name = hitInfo.collider.gameObject.tag;
                if (name != "Player")
                {
                    //Debug.Log("反方向退回去");
                    //Debug.DrawLine(transform.position, transform.position + gameObject.transform.forward * (-1) * distance,Color.green, distance);
                    //如果射线碰撞的不是相机，那么就取得 相机到射线碰撞点 的距离
                    float backDistance = Vector3.Distance(transform.position, hitInfo.point);
        
                    float tempDistance = distance + backDistance;
                    if (tempDistance <= lockDistance)
                    {
                        distance = Mathf.Lerp(distance, tempDistance, Time.deltaTime * 10f);
                    }

                    CameraState = 2;//backState
                    return false;
                }
                //Debug.DrawLine(transform.position, transform.position + gameObject.transform.forward * (-1) * distance,Color.yellow, distance);
                //Debug.Log("name == Player");
            }
            //背后没墙 true（）安心退
            else
            {
                //Debug.Log("back no walls");
                //distance = lockDistance;
                distance = Mathf.Lerp(distance,lockDistance,Time.deltaTime*10f);
                CameraState = 0;
                return true;
            }
        }
        //背后没墙 true（）安心退
        else
        {
            //Debug.Log("back no walls");
            //distance = lockDistance;
            distance = Mathf.Lerp(distance, lockDistance, Time.deltaTime * 10f);
            CameraState = 0;
            return true;
        }
        CameraState = 0;
        return true;
    }
    
    void Rotate()
    {
        float angleChange = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;//鼠标在X轴上移动的距离乘以旋转系数得到的旋转角度
        rot -= angleChange;//更新横向角度
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
        //正在因为墙壁而被迫scroll的时候不能改距离
        if (distance != lockDistance)
        {
            return;;
        }
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

        lockDistance = distance;
    }
   
}
