using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGoAround : MonoBehaviour
{
    public float zoomSensitivity = 10f;
    public float mouseSensitivity = 5f;
    public float speedSensitivity = 20f;
    private float m_deltX = 0f;
    private float m_deltY = 0f;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        m_deltX = mainCamera.transform.rotation.eulerAngles.x;
        m_deltY = mainCamera.transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            LockCursor(true);
            UFOMove();
            ZoomMove();
        }
        else LockCursor(false);
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            LookRotation();
        }
    }
    private void ZoomMove()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            mainCamera.transform.localPosition = mainCamera.transform.position + mainCamera.transform.forward * Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity; ;
        }
    }
    private void UFOMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            horizontal *= 3; vertical *= 3;
        }
        mainCamera.transform.Translate(Vector3.forward * vertical * speedSensitivity * Time.deltaTime);
        mainCamera.transform.Translate(Vector3.right * horizontal * speedSensitivity * Time.deltaTime);
    }
    private void LookRotation()
    {
        m_deltX += Input.GetAxis("Mouse X") * mouseSensitivity;
        m_deltY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        m_deltX = ClampAngle(m_deltX, -360, 360);
        m_deltY = ClampAngle(m_deltY, -70, 70);
        mainCamera.transform.rotation = Quaternion.Euler(m_deltY, m_deltX, 0);
    }
    private void LockCursor(bool b)
    {
        //Cursor.lockState = b ? CursorLockMode.Locked : Cursor.lockState = CursorLockMode.None;
        Cursor.visible = b ? false : true;
    }
    float ClampAngle(float angle, float minAngle, float maxAgnle)
    {
        if (angle <= -360)
            angle += 360;
        if (angle >= 360)
            angle -= 360;

        return Mathf.Clamp(angle, minAngle, maxAgnle);
    }


}
