using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float rotSpeed;
    public float groundDrag;
    
    [Header(("camera"))]
    private bool changeCameraFlag = false;
    public float changeCameratimer;
    private float changeCameratimerTemp;
    // private bool canChangeCamera;
    public GameObject cameraFirstPer;
    public GameObject cameraThirdPer;
    
    [Header("Anim")]
    private Animator PlayerAnimator;

    public Rigidbody rb;
    public YStateMachine stateMachine => GetComponent<YStateMachine>();
    
    public void Awake()
    {
        InitStateMachine();
    }

    void InitStateMachine()
    {
        var states = new Dictionary<Type, YBaseState>
        {
            {typeof(YPlayerIdleState),new YPlayerIdleState(this)},
            {typeof(YPlayerRunState),new YPlayerRunState(this) },
            {typeof(YPlayerBeHitState),new YPlayerBeHitState(this) }
        };
        GetComponent<YStateMachine>().SetStates(states);
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = GetComponentInChildren<Animator>();
        PlayerAnimator.SetInteger("AnimState",0);
        
        //camera
        changeCameratimerTemp = 0f;
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //切换相机
        if (changeCameratimerTemp <= 0)
        {
            if (Input.GetKey(KeyCode.U))
            {
                changeCameraFlag = !changeCameraFlag;
                cameraFirstPer.SetActive(changeCameraFlag);
                cameraThirdPer.SetActive(!changeCameraFlag);
                changeCameratimerTemp = changeCameratimer;
            }
        }
        else
        {
            changeCameratimerTemp -= Time.deltaTime;
        }
    }
}
