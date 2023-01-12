using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YchangeCharacter : MonoBehaviour
{
    //public int characterIndex;
    private int curIndedx;
    public GameObject[] characterArr;
    public GameObject[] charUIFightHeadArr;
    public GameObject[] charUIWaitHeadArr;
    public YPlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        curIndedx = 0;
        playerMovement = gameObject.GetComponent<YPlayerMovement>();
    }

    // Update is called oc··e per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchTCharacter(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchTCharacter(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchTCharacter(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            switchTCharacter(3);
        }
    }

    public void switchTCharacter(int index)
    {
        if (index!=curIndedx&& characterArr[index]&&characterArr[curIndedx])
        {
            characterArr[index].SetActive(true);
            characterArr[curIndedx].SetActive(false);

            if (charUIFightHeadArr[index] && charUIWaitHeadArr[curIndedx])
            {
                //UI方面 将出战角色的图调整为out(fIGHT)，回去的调整为WAIT
                charUIFightHeadArr[index].SetActive(true);
                charUIWaitHeadArr[index].SetActive(false);
            
                charUIFightHeadArr[curIndedx].SetActive(false);
                charUIWaitHeadArr[curIndedx].SetActive(true);
            }
            
            curIndedx = index;
            playerMovement.changeCharWhenSwitch();
        }
    }
}
