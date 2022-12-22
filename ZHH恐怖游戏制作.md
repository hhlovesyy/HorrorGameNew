## ZHH恐怖游戏制作



## 一.ShaderGraph篇

### 1.配置ShaderGraph

要在Unity当中首先安装Shader Graph和**URP管线**才可以(先用URP来做,HDRP会出现一些问题)

### 1.Glitch shader

用Shader Graph实现的Glitch效果.

新建一个Lit Shader Graph(在URP管线下),然后这样连线:







## 二.C# 脚本篇

### 1.关于Trigger触发某些物体出现和某些物体消失的脚本

- YTriggerToMakeSth.cs文件,里面有一些相关字段,**这个脚本可能会有bug,后面遇到了再去修复**



### 2.光的Flitch效果

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerEffect : MonoBehaviour
{
    [Tooltip("External light to flicker; you can leave this null if you attach script to a light")]
    public new Light light;
    [Tooltip("Minimum random light intensity")]
    public float minIntensity = 0f;
    [Tooltip("Maximum random light intensity")]
    public float maxIntensity = 1f;
    [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
    [Range(1, 50)]
    public int smoothing = 5;

    // Continuous average calculation via FIFO queue
    // Saves us iterating every time we update, we just change by the delta
    Queue<float> smoothQueue;
    float lastSum = 0;


    /// <summary>
    /// Reset the randomness and start again. You usually don't need to call
    /// this, deactivating/reactivating is usually fine but if you want a strict
    /// restart you can do.
    /// </summary>
    public void Reset()
    {
        smoothQueue.Clear();
        lastSum = 0;
    }

    void Start()
    {
        smoothQueue = new Queue<float>(smoothing);
        // External or internal light?
        if (light == null)
        {
            light = GetComponent<Light>();
        }
    }

    void Update()
    {
        if (light == null)
            return;

        // pop off an item if too big
        while (smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        // Calculate new smoothed average
        light.intensity = lastSum / (float)smoothQueue.Count;
    }
}

```

- 下午:把光的Flitch效果和电池电量下降联系到一起,加一个简单的UI,看懂目前项目的代码和Shader
