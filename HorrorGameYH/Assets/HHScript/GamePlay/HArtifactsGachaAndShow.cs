using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HArtifactsGachaAndShow : MonoBehaviour
{
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        character.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0,100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
