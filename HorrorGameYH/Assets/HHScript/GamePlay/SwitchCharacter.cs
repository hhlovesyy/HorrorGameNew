using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] characters;
    public Transform nowTransform;
    public int nowCharacterIndex;
    public GameObject[] orientations;
    public Transform nowOrientationTransform;
    
    void Start()
    {
        characters[0].gameObject.SetActive(true);
        nowTransform = characters[0].transform;
        nowCharacterIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        nowTransform = characters[nowCharacterIndex].gameObject.transform;
        nowOrientationTransform = orientations[nowCharacterIndex].transform;
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < 4; i++)
            {
                characters[i].gameObject.SetActive(false);
            }
            characters[0].gameObject.SetActive(true);
            nowCharacterIndex = 0;
            print(nowOrientationTransform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < 4; i++)
            {
                characters[i].gameObject.SetActive(false);
            }
            characters[1].gameObject.SetActive(true);
            nowCharacterIndex = 1;
            print(nowOrientationTransform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i < 4; i++)
            {
                characters[i].gameObject.SetActive(false);
            }
            characters[2].gameObject.SetActive(true);
            nowCharacterIndex = 2;
            print(nowOrientationTransform.rotation);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            for (int i = 0; i < 4; i++)
            {
                characters[i].gameObject.SetActive(false);
            }
            characters[3].gameObject.SetActive(true);
            nowCharacterIndex = 3;
            print(nowOrientationTransform.rotation);
        }
        
        characters[nowCharacterIndex].transform.position = nowTransform.position;
        characters[nowCharacterIndex].transform.rotation = nowTransform.rotation;
        for (int i = 0; i < 4; i++)
        {
            orientations[i].transform.forward = nowOrientationTransform.forward;
            orientations[i].transform.right = nowOrientationTransform.right;
        }
    }
}
