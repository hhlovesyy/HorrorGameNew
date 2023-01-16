using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HArtifactsGachaAndShow : MonoBehaviour
{
    public GameObject character;
    public Material[] artifactMaterials;

    public GameObject originMat;

    private int artifactPosIndex;
    // Start is called before the first frame update
    void Start()
    {
        Mesh m = character.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        string name = m.GetBlendShapeName(0);
        print(name);
        int blendShapeIndex = m.GetBlendShapeIndex("まばたき");
        print(blendShapeIndex);
        character.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(blendShapeIndex,100);
    }

    //刷出圣遗物的位置,并更新对应的图片资源
    public void RollingArtifactLocation()
    {
        artifactPosIndex = Random.Range(0, 4); //随机刷出圣遗物的位置
        originMat.gameObject.GetComponent<MeshRenderer>().material = artifactMaterials[artifactPosIndex];
    }

    //刷出圣遗物的主词条
    public void RollingArtifactOrigin(int index)
    {
        HArtifactAttr randomArtifact = HArtifactManager.instance.RollArtifactOrigin(index);
    }
    
    //随机刷出一个圣遗物的相关逻辑
    public void RollingARandomArtifact()
    {
        RollingArtifactLocation();
        RollingArtifactOrigin(artifactPosIndex);
        //HArtifactProbabilityInfo.GetInstance(). 写一个相关函数
    }

    public void StrengthenAnArtifact()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
