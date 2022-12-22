using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HFlashLightChange : MonoBehaviour
{
    public bool isOn;
    public GameObject lightSource;
    private float energy;
    private bool energyGettingDown;
    private Light spotLightAttr;
    public float energyGettingDownSpeed = 100f;
    public float energyDownInterval = 5f; //�����½��ķ���,һ���½�����energy

    private bool canUseFlashLight;

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        lightSource.SetActive(isOn);

        energy = 1000f;
        energyGettingDown = false;
        spotLightAttr = lightSource.gameObject.GetComponent<Light>();
        if (!spotLightAttr)
        {
            Debug.LogError("�ֵ�Ͳ�����Ĺ�û��light����!����!");
        }
        canUseFlashLight = true;
    }

    void SettingLight(int energyVal)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FKey"))
        {
            if (!canUseFlashLight) return;
            isOn = !isOn;
            lightSource.SetActive(isOn);
            if (isOn && energy > 0)  //��������ֵ�Ͳ,��ʼ��������
            {
                energyGettingDown = true;
            }
            else
            {
                energyGettingDown = false;
            }
        }
        if (energyGettingDown)
        {
            //Debug.Log(Time.deltaTime);
            energy -= (int)(Time.deltaTime * energyGettingDownSpeed);
            //Debug.Log(energy);
            if (energy <= 0)
            {
                energy = 0;
                isOn = false;
                lightSource.SetActive(isOn);
                canUseFlashLight = false;
            }

            if (energy % 100 == 0)
            {
                //Debug.Log("Biscuit!"+energy.ToString());
            }
            
        }

    }

}
