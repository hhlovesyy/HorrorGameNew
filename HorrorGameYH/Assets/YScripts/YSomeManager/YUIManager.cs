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
    //IEnumerator flash()
    //{
    //    BloodImage.color = flashColor;
    //    yield return new WaitForSeconds(0.25f);
    //    BloodImage.color = defaultColor;
    //}

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = shakeCamera.transform.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;
            //shakeCamera.transform.position = new Vector3(x, y, -10f);
            shakeCamera.transform.position += new Vector3(x, y, z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        shakeCamera.transform.position = orignalPosition;
    }

    public void cameraShakeFunc()
    {
        StartCoroutine(Shake(2f, 0.01f));
    }

    private void Update()
    {
        if(beginFlash)
        {
            flashTimerTemp -= Time.deltaTime * flashStep;
            if(flashTimerTemp > 1)
            {
                BloodImage.color =
                Color.Lerp(flashColor, defaultColor, flashTimerTemp);
            }
            else if (flashTimerTemp <=1&& flashTimerTemp > 0)
            {
                BloodImage.color =
                Color.Lerp(defaultColor, flashColor, flashTimerTemp);
            }
            else if (flashTimerTemp <= 0) 
            {
                Debug.Log("flashOver");
                beginFlash = false;
                flashTimerTemp = flashTimer;
                BloodImage.color = defaultColor;
            }
        }
        
    }


}
