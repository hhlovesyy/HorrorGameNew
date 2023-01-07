using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YUIManager : MonoBehaviour
{
    public Image pBloodImage;
    public static Image BloodImage => Instance.pBloodImage;

    public Color defaultColor;
    public Color flashColor;
    public float flashTimer=2f;
    public float flashTimerTemp=2f;
    public bool beginFlash;
    public float flashStep;
    public bool bShakeCamera;
    //public float magnitude = 10000f;

    private Vector3 v3Shake = new Vector3(-0.5f,0.6f,0f);
    public Vector3 defaultv3 = new Vector3(0,0,0);
    public Camera shakeCamera;
    public static YUIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Start()
    {
        defaultColor = BloodImage.color;
    }

    public static object getInstance()
    {
        return Instance;
    }

    public void flashScreen()
    {
        beginFlash = true;
        //StartCoroutine(flash());
    }
    public void shakeScreen()
    {
        bShakeCamera = true;
    }
    //IEnumerator flash()
    //{
    //    BloodImage.color = flashColor;
    //    yield return new WaitForSeconds(0.25f);
    //    BloodImage.color = defaultColor;
    //}

    //public IEnumerator Shake(float duration, float magnitude)
    //{
    //    Vector3 orignalPosition = shakeCamera.transform.position;
    //    float elapsed = 0f;
    //    while (elapsed < duration)
    //    {
    //        float x = Random.Range(-1f, 1f) * magnitude;
    //        float y = Random.Range(-1f, 1f) * magnitude;
    //        float z = Random.Range(-1f, 1f) * magnitude;
    //        //shakeCamera.transform.position = new Vector3(x, y, -10f);
    //        shakeCamera.transform.position += new Vector3(x, y, z);
    //        elapsed += Time.deltaTime;
    //        yield return 0;
    //    }
    //    shakeCamera.transform.position = orignalPosition;
    //}

    //public IEnumerator Shake(float duration)
    //{
    //    //Vector3 orignalPosition = shakeCamera.transform.position;
    //    float elapsed = 0f;
    //    while (elapsed < duration)
    //    {
    //        //float x = Random.Range(-1f, 1f) * magnitude;
    //        //float y = Random.Range(-1f, 1f) * magnitude;
    //        //float z = Random.Range(-1f, 1f) * magnitude;
    //        ////shakeCamera.transform.position = new Vector3(x, y, -10f);
    //        //shakeCamera.transform.position += new Vector3(x, y, z);
    //        elapsed += Time.deltaTime;
    //        yield return 0;
    //    }
    //    //shakeCamera.transform.position = orignalPosition;
    //}
    //public void cameraShakeFunc()
    //{
    //    //StartCoroutine(Shake(20f, 0.01f));
    //    bShakeCamera = true;
    //    StartCoroutine(Shake(1f));
    //    //bShakeCamera = false;
    //}

    private void Update()
    {
        if(beginFlash)
        {
            Debug.Log(shakeCamera.transform.position.x);
            flashTimerTemp -= Time.deltaTime * flashStep;
            if(flashTimerTemp > 1)
            {
                BloodImage.color =
                Color.Lerp(flashColor, defaultColor, flashTimerTemp);

                if(bShakeCamera)
                shakeCamera.transform.localPosition
                    = Vector3.Lerp(defaultv3, v3Shake, flashTimerTemp);
            }
            else if (flashTimerTemp <=1&& flashTimerTemp > 0)
            {
                BloodImage.color =
                Color.Lerp(defaultColor, flashColor, flashTimerTemp);

                if (bShakeCamera)
                    shakeCamera.transform.localPosition
                   = Vector3.Lerp(v3Shake, defaultv3, flashTimerTemp);
            }
            else if (flashTimerTemp <= 0) 
            {
                Debug.Log("flashOver");
                beginFlash = false;
                flashTimerTemp = flashTimer;
                BloodImage.color = defaultColor;

                shakeCamera.transform.localPosition = defaultv3;
            }
        }

        //if (bShakeCamera)
        //{
        //    flashTimerTemp -= Time.deltaTime * flashStep;
        //    if (flashTimerTemp > 1)
        //    {
        //        shakeCamera.transform.position 
        //            = Vector3.Lerp(defaultv3, v3Shake, flashTimerTemp);
        //    }
        //    else if (flashTimerTemp <= 1 && flashTimerTemp > 0)
        //    {
        //        shakeCamera.transform.position
        //           = Vector3.Lerp(v3Shake, defaultv3, flashTimerTemp);
        //    }
        //    else if (flashTimerTemp <= 0)
        //    {
        //        Debug.Log("shakeOver");
        //        beginFlash = false;
        //        flashTimerTemp = flashTimer;
        //        shakeCamera.transform.position = defaultv3;
        //    }
        //}

        //if(bShakeCamera)
        //{
        //    //Vector3 orignalPosition = shakeCamera.transform.position;
        //    float x = Random.Range(-1f, 1f) * magnitude;
        //    float y = Random.Range(-1f, 1f) * magnitude;
        //    float z = Random.Range(-1f, 1f) * magnitude;
        //    //shakeCamera.transform.position = new Vector3(x, y, -10f);
        //    shakeCamera.transform.position += new Vector3(x, y, z);
        //    Debug.Log(shakeCamera.transform.position.x);
        //}

    }


}
