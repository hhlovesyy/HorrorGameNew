using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class YItemForQuest : MonoBehaviour
{
    public bool isEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (!isEnter&&other.CompareTag("Player"))
        {
            isEnter = true;
            YPlayerProp.instance.itemAmount += 1;
            Debug.Log(YPlayerProp.instance.itemAmount);
            Destroy(gameObject);
            
        }
    }
}
