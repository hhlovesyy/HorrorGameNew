using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HArtifactsUIManager : MonoBehaviour
{
    public static HArtifactsUIManager instance;
    public GameObject detailPanelUIGroups;  //详情界面的UI组
    public GameObject rollingGroups;
    public GameObject avatar;
    
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

    
    public void SetUIStates(ArtifactShowState nowState)
    {
        switch (nowState)
        {
            case ArtifactShowState.IdlingRotate:
                detailPanelUIGroups.gameObject.SetActive(false);
                break;
            case ArtifactShowState.ClickForDetail:
                detailPanelUIGroups.gameObject.SetActive(true);
                break;
            case ArtifactShowState.RollingAnArtifact:
                detailPanelUIGroups.gameObject.SetActive(false);
                rollingGroups.gameObject.SetActive(true);
                avatar.gameObject.SetActive(false);
                break;
                
        }
    }
    void Start()
    {
        detailPanelUIGroups.gameObject.SetActive(false);
        rollingGroups.gameObject.SetActive(false);
        avatar.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
