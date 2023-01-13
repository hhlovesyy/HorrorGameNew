using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YCameraManager : MonoBehaviour
{
    public static YCameraManager instance;
    [Header(("camera"))]
    private bool changeCameraFlag = false;

    public float changeCameratimer;
    private float changeCameratimerTemp;
    // private bool canChangeCamera;

    public GameObject cameraFirstPer;
    public GameObject cameraThirdPer;
    public Camera DialogCamera;

    public bool isDialog;
    public GameObject playerModel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        changeCameratimerTemp = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDialog)
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
    public void changeDialogCamera(Transform target,bool bDialog)
    {
        isDialog = bDialog;
        if (isDialog)
        {
            cameraFirstPer.SetActive(false);
            cameraThirdPer.SetActive(false);
            
            if(playerModel)playerModel.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            DialogCamera.gameObject.SetActive(true);
            DialogCamera.transform.position = target.position + target.forward * 0.9f;
            DialogCamera.transform.LookAt(target);
        }
        else
        {
            if (playerModel) playerModel.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            cameraFirstPer.SetActive(changeCameraFlag);
            cameraThirdPer.SetActive(!changeCameraFlag);
            DialogCamera.gameObject.SetActive(false);
        }
    }
    
}
