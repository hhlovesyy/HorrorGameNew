using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个脚本用来写一些用在UI上或者是类似UI组件上的效果,比如始终朝向屏幕的效果,有需要就把这个脚本挂载到对应的GO下面
public class HUIShowEffects : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("是否面向屏幕")]
    public bool isFacingScreen; //是否面向屏幕
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingScreen)
        {
            transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);
        }
    }
}
