using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HExcelConfig 
{
    public static readonly string excelsFolderPath = Application.dataPath + "/DesignTablesHDesignTables/";
    public static readonly string assetPath = "Assets/HResources/Release/";
}

public class HReadDesignTables : MonoBehaviour
{
    public TextAsset[] textAssets;
    
    // Start is called before the first frame update
    void Start()
    {
        print("now we are in start");
        for (int i = 0; i < textAssets.Length; i++)
        {
            TextAsset textAsset = textAssets[i];
            string[] rows = textAsset.text.Split('\n');
            string name = textAsset.name;
            getMsgFromTextAsset(name,rows);
        }    
    }

    void getMsgFromTextAsset(string name,string[] rows)
    {
        print("name===" + name);
        switch (name)
        {
            case "artifactsProbabilityDetail":
               HArtifactProbabilityInfo.GetInstance().SetArtifactProbabilityInfo(rows);
                break;
            //case 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
