using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HMinimapSimple : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera minicamera;
    public Transform player;
    public Transform miniplayerIcon;
    public Transform miniplayerLookAtIcon;

    public Transform playerOrientation;
    private bool isLargeMinimap;
    public Transform largeMinimap;
    void Start()
    {
        isLargeMinimap = false;
        largeMinimap.gameObject.SetActive(isLargeMinimap);
    }

    void CheckInputChangeSizeMinimap()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isLargeMinimap = !isLargeMinimap;
            largeMinimap.gameObject.SetActive(isLargeMinimap);
        }
    }

    // Update is called once per frame
    void Update()
    {
        minicamera.transform.position = new Vector3(player.position.x, minicamera.transform.position.y, player.position.z);
        miniplayerIcon.eulerAngles = new Vector3(0, 0, -player.eulerAngles.y);
        //print(playerOrientation.eulerAngles.y);
        miniplayerLookAtIcon.eulerAngles = new Vector3(0, 0, -playerOrientation.eulerAngles.y+45);
  
        CheckInputChangeSizeMinimap();
        
    }
}
