using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HFlashLightChange : MonoBehaviour
{
    public bool isOn;
    public GameObject lightSource;
    private int energy;
    private bool energyGettingDown;
    public float energyGettingDownSpeed = 100f;
    public float energyDownInterval = 5f; //�����½��ķ���,һ���½�����energy

    private bool canUseFlashLight;
    private LightFlickerEffect lightFlickereffect;
    private int count = 0;

    public void GettingABattery()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        lightSource.SetActive(isOn);
        energy = 1000;
        energyGettingDown = false;
        canUseFlashLight = true;
        lightFlickereffect = lightSource.gameObject.GetComponent<LightFlickerEffect>();
        if (!lightFlickereffect)
        {
//            Debug.LogError("��Դ����û��Flicker�ű�!����!");
        }
        count = 0;
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
            count++;
            energy -= (int)(Time.deltaTime * energyGettingDownSpeed);
            //Debug.Log(energy);
            if (energy <= 0)
            {
                energy = 0;
                isOn = false;
                lightSource.SetActive(isOn);
                canUseFlashLight = false;
            }

            if (count % 100 == 0)
            {
                //Debug.Log("Biscuit!"+energy.ToString());
                if (lightFlickereffect)
                {
                    lightFlickereffect.SetLightAttr(energy);
                }
            }
            
        }

    }

}
