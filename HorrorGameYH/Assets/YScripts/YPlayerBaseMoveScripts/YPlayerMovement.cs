using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class YPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float rotSpeed;
    public float groundDrag;
    public GameObject playerGo; 

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;


    //��ֹ����̫ƽ��������Ƿ��ǵ��� �ǵĻ��Ͱ��������� ������������
    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;


    Rigidbody rb;
    float horizontalInput;
    float verticalInput;
    public Transform orientation;
    Vector3 moveDiretion;

    public Animator PlayerAnimator;

    [Header(("camera"))]
    //private bool changeCameraFlag = false;
    private bool changeCameraFlag = true;
    
    public GameObject cameraFirstPer;
    public GameObject cameraThirdPer;
    
    // Start is called before the first frame update
    void Start()
    {
        readyToJump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        //groundDrag = 5f;
        //playerHeight = 2f;
        //whatIsGround = LayerMask.NameToLayer("WhatIsGround");
        //Debug.Log(LayerMask.NameToLayer("WhatIsGround"));
        changeCameraFlag = true;
    }
    void Update()
    {
        grounded = Physics.Raycast(transform.position,Vector3.down,playerHeight*0.5f+0.2f,whatIsGround);
        myInput();
        SpeedControl();

        if (grounded)
        {
            //Debug.Log("gggg");
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
            
    }
    void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontalInput)>0.01f ||Mathf.Abs( verticalInput)>0.01f)
        {
            PlayerAnimator.SetInteger("AnimState",1);
            
            moveDiretion = orientation.forward * verticalInput + orientation.right * horizontalInput;

            //ʹ����Ҫ�ƶ�ʱ������Ҫ��ת�ķ����ұ�����⾵ͷ����ջ��ߵ���ʱ����ɫҲ����ת��
            //[�����ƶ�ʱ���泯�ƶ�������ת_CXW30�Ĳ���-CSDN����_�ƶ������ǰ����ô����](https://blog.csdn.net/qq_32605447/article/details/90693227)
            //[Unity Vector3��Quaternion�໥ת��_Parkergh�Ĳ���-CSDN����_quaternionתvector3](https://blog.csdn.net/m0_37763682/article/details/107461513)
            Quaternion lookRot = Quaternion.LookRotation(moveDiretion);    //dirΪǰ���ڵ��pos
            Vector3 lookR = lookRot.eulerAngles;
            //Ҳ����˵ ��ɫֻ������y��ת��
            lookR = new Vector3(0f,lookR.y,0f);
            lookRot = Quaternion.Euler(lookR);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Mathf.Clamp01(rotSpeed * Time.deltaTime));
        }
        else
        {
            PlayerAnimator.SetInteger("AnimState",0);
        }

        //ʲôʱ����
        if(Input.GetKey(jumpKey)&&readyToJump&&grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(resetJump),jumpCooldown);
        }

        if (Input.GetKey(KeyCode.U))
        {
            changeCameraFlag = !changeCameraFlag;
            cameraFirstPer.SetActive(changeCameraFlag);
            cameraThirdPer.SetActive(!changeCameraFlag);
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        //�����ƶ�����
        moveDiretion = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if(grounded)
            rb.AddForce(moveDiretion.normalized * moveSpeed * 10f, ForceMode.Force);
        if (!grounded)
            rb.AddForce(moveDiretion.normalized*moveSpeed*10f*airMultiplier,ForceMode.Force);
        
        
        
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //�����ƶ��ٶ�
        if(flatVel.magnitude>moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            //rb.velocity = new Vector3(limitedVel.x,rb.velocity.y,limitedVel.z);
            rb.velocity = new Vector3(limitedVel.x,0f,limitedVel.z);
        }
    }
    public void Jump()
    {
        //����y�� ����ÿ�β�����һ���� !!!
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    public void resetJump()
    {
        readyToJump = true;
    }
}
