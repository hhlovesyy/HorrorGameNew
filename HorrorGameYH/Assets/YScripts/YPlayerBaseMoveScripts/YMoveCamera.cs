using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YMoveCamera : MonoBehaviour
{
    //此代码功能：让相机位置永远位于角色眼睛部分

    public Transform cameraPos;
    private float magnitude = 0.2f;

    public bool bShakeCamera;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPos.position;

        if(bShakeCamera)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;
            //shakeCamera.transform.position = new Vector3(x, y, -10f);
            transform.position += new Vector3(x, y, z);
        }
    }

    public void cameraShakeFunc()
    {
        //StartCoroutine(Shake(20f, 0.01f));
        bShakeCamera = true;
        StartCoroutine(Shake(1f));

    }

    public IEnumerator Shake(float duration)
    {
        //Vector3 orignalPosition = shakeCamera.transform.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            //float x = Random.Range(-1f, 1f) * magnitude;
            //float y = Random.Range(-1f, 1f) * magnitude;
            //float z = Random.Range(-1f, 1f) * magnitude;
            ////shakeCamera.transform.position = new Vector3(x, y, -10f);
            //shakeCamera.transform.position += new Vector3(x, y, z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        //shakeCamera.transform.position = orignalPosition;

        bShakeCamera = false;
    }
}
