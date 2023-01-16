using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class HArtifactProbabilityInfo
{
    public static HArtifactProbabilityInfo instance;
    public string[] fieldnames;
    private string[] fieldEnglishNames;
    public float[,] artifactProbabilities;
    public int rowLen;
    public int colLen;
    

    // 定义私有构造函数，使外界不能创建该类实例
    private HArtifactProbabilityInfo()
    {
    }

    /// <summary>
    /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
    /// </summary>
    /// <returns></returns>
    public static HArtifactProbabilityInfo GetInstance()
    {
        // 如果类的实例不存在则创建，否则直接返回
        if (instance == null)
        {
            instance = new HArtifactProbabilityInfo();
        }

        return instance;
    }

    public void SetArtifactProbabilityInfo(string[] rows)
    {
        Debug.Log("Print design table info....");
        rowLen = rows.Length;
        colLen = rows[0].Split(',').Length;
        Debug.Log("策划表的行数和列数分别为:::");
        Debug.Log(rowLen);
        Debug.Log(colLen);

        fieldnames = rows[0].Split(',');
        fieldEnglishNames = rows[1].Split(',');
        artifactProbabilities = new float[rowLen-3, colLen];
        

        // 存储所有圣遗物的所有词条的概率值,并存在artifactProbabilities数组当中,
        //todo:存储方法比较笨,后面值得优化一下,比如用List存
        for (int i = 3; i < rowLen; i++)
        {
            string[] arow = rows[i].Split(',');
            for (int j = 2; j < colLen; j++)
            {
                Debug.Log("===before====");
                Debug.Log(i);
                Debug.Log(j);
                artifactProbabilities[i - 3,j - 2] = float.Parse(arow[j]);
            }
        }

        // for (int i = 0; i < rowsNum-3; i++)
        // {
             // for (int j = 0; j < colsNum-2; j++)
             // {
             //     Debug.Log(artifactProbabilities[0,j]);
             // }
        // }

    }
}

public class HArtifactManager : MonoBehaviour
{
    //单例模式
    public static HArtifactManager instance;
    public float[,] probabilityInfo;
    private HArtifactAttr artifact;

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

    public HArtifactAttr RollArtifactOrigin(int index)
    {
        string timeSeed = System.DateTime.Now.ToString() + System.DateTime.Now.Millisecond.ToString();
        Random.InitState(timeSeed.GetHashCode());
        
        probabilityInfo = HArtifactProbabilityInfo.GetInstance().artifactProbabilities;
        int colLen = HArtifactProbabilityInfo.GetInstance().colLen;

        float randomNum = Random.Range(0,10000)/100.0f;
        // print("randomNumber===");
        // print(randomNum);

        Dictionary<string, int> artifactDict=new Dictionary<string, int>();

        //1.圣遗物初始的主词条
        int mainWordEntryIndex=0;
        

        float tmpSum = 0;
        for (int i = 0; i < colLen; i++)
        {
            tmpSum += probabilityInfo[index, i];
            if (tmpSum > randomNum)
            {
                mainWordEntryIndex = i;
                break;
            }
        }

        //向圣遗物中加入主词条,value值暂定为-1(为了区分出主词条)
        string mainWordEntryName = HArtifactProbabilityInfo.GetInstance().fieldnames[mainWordEntryIndex + 2];
        artifactDict.Add(mainWordEntryName,-1);
        
        //圣遗物的几个副词条
        int wordEntryNCnt; //圣遗物有几个词条,todo:暂时写死了概率,80%三个词条,20%4个词条
        int tmpRnd = Random.Range(0, 100);
        if (tmpRnd < 80) wordEntryNCnt = 3;
        else wordEntryNCnt = 4;
        
        //下面的逻辑用于随机化出来一些圣遗物副词条
        List<float> tmpList = new List<float>();  //存储当前状态下的各个概率
        List<string> tmpNameList = new List<string>(); //存储副词条的名字
        float probabilitySum = 0;

        for (int i = 0; i < colLen-2; i++)
        {
            probabilitySum += probabilityInfo[index + 5, i];
            tmpList.Add(probabilityInfo[index+5,i]);
            tmpNameList.Add(HArtifactProbabilityInfo.GetInstance().fieldnames[i + 2]);
        }
        
        //从副词条的待选方案中去掉主词条
        tmpNameList.RemoveAt(mainWordEntryIndex);
        tmpList.RemoveAt(mainWordEntryIndex);
        probabilitySum -= probabilityInfo[index + 5, mainWordEntryIndex];
        int subWordEntryIndex = 0;
        
        while (wordEntryNCnt>0)
        {
            float tempSum = 0;
            float tmprandomNum = Random.Range(0,100*probabilitySum)/100.0f;
            print("tmprandomNum....");
            print(tmprandomNum);
            print("name and value");
            for (int i = 0; i < tmpList.Count; i++)
            {
                tempSum += tmpList[i];
                if (tempSum > tmprandomNum)
                {
                    subWordEntryIndex = i;
                    break;
                }
                //print for debug
                
                print(tmpNameList[i]);
                print(tmpList[i]);
            }
            
            print("subWordEntryIndex....");
            print(subWordEntryIndex);
            string subArtifactName = tmpNameList[subWordEntryIndex];

            int randomValueIndex = Random.Range(0, 4); //每个副词条有四种初始可能性,比如暴击有2.7,3.1,3.5,3.9四种
            artifactDict.Add(subArtifactName,randomValueIndex); //数值也要roll出来,一开始设定为0就行
            //从List中扣除掉这个属性
            probabilitySum -= tmpList[subWordEntryIndex];
            tmpList.RemoveAt(subWordEntryIndex);
            tmpNameList.RemoveAt(subWordEntryIndex);
            
            wordEntryNCnt--;
            
        }

        print("now let's print out the possibilities");
        print("这个圣遗物对应的位置是:");
        print(index);
        print("策划表索引:");
        print(mainWordEntryIndex);
        
        print("这个圣遗物的主副词条分别是:");
        foreach (var keyValuePair in artifactDict)
        {
            print(keyValuePair.Key+"|"+keyValuePair.Value.ToString());
        }
        MakeArtifactValue(artifactDict);
        //todo:现在artifactDict里存的字段并不是初始策划表里的列索引,后面需要改成列索引然后去找到对应的概率然后返回
        
        
        return artifact;
    }

    void MakeArtifactValue(Dictionary<string,int> dict)
    {
        
    }
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
