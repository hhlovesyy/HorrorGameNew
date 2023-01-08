using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YchangeCharacter : MonoBehaviour
{
    public int characterIndex;
    private int curIndedx;
    public GameObject[] characterArr;

    public YPlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        characterIndex = 0;
        curIndedx = 0;
        playerMovement = gameObject.GetComponent<YPlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            characterIndex = 0;
            switchTCharacter(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            characterIndex = 1;
            switchTCharacter(1);
        }
    }

    public void switchTCharacter(int index)
    {
        if (characterArr[index]&&characterArr[curIndedx])
        {
            characterArr[index].SetActive(true);
            characterArr[curIndedx].SetActive(false);
            curIndedx = index;
            playerMovement.changeCharWhenSwitch();
        }
    }
}
