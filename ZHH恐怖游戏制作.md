## ZHH恐怖游戏制作



## 一.ShaderGraph篇

### 1.配置ShaderGraph

要在Unity当中首先安装Shader Graph和**URP管线**才可以(先用URP来做,HDRP会出现一些问题)

### 1.Glitch shader

用Shader Graph实现的Glitch效果.

(1)新建一个Lit Shader Graph(在URP管线下),然后这样连线:

<img src="ZHH%E6%81%90%E6%80%96%E6%B8%B8%E6%88%8F%E5%88%B6%E4%BD%9C.assets/image-20221223154919550.png" alt="image-20221223154919550" style="zoom: 67%;" />

上图的操作是,将UV的G通道提取出来(提取完之后应该是一维的数据,参考Split官方文档,Split之后的结果的每一行是一个单独的值,对应纹理的V坐标),将Split之后的结果对Simple Noise结点进行采样,**此时需要注意的是由于Unity Shader Graph中对纹理采样需要一个二维的向量,因此会把G通道的值复制两边成为(G,G)向量,再去进行采样**,所以实际上上图采样后的结果的每一行的纹素值对应初始噪声图(X,X)位置的纹理坐标.上面这张图的连线等同于下面这张图:

<img src="ZHH%E6%81%90%E6%80%96%E6%B8%B8%E6%88%8F%E5%88%B6%E4%BD%9C.assets/image-20221223160119340.png" alt="image-20221223160119340" style="zoom:80%;" />



(2)进行Remap操作,并添加一个Time采样的结点

![image-20221223162444157](ZHH%E6%81%90%E6%80%96%E6%B8%B8%E6%88%8F%E5%88%B6%E4%BD%9C.assets/image-20221223162444157.png)





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





- 2023.1.9 todo:
  - (1)小地图功能:[(23条消息) Unity小地图Minimap制作全面功能介绍篇_紫龙大侠的博客-CSDN博客](https://blog.csdn.net/alayeshi/article/details/115913174)
  - (2)抽卡系统:[关于抽卡，作为策划我所知道的一切 - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/356187524),尝试复现一下,资源需要找一找
  - (3)打开宝箱触发宝箱消融的shader效果
