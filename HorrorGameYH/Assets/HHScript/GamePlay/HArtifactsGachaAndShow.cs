using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HArtifactsGachaAndShow : MonoBehaviour
{
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        Mesh m = character.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        string name = m.GetBlendShapeName(0);
        print(name);
        int blendShapeIndex = m.GetBlendShapeIndex("");
        character.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(blendShapeIndex,100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
