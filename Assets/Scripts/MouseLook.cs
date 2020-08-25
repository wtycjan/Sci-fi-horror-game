using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;
    float yRotation = 0f;

    public bool blockY = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,- 90f,  90f);

        yRotation -= mouseX;
        /*if (blockY)
        { 
            yRotation = Mathf.Clamp(yRotation, yRotation -90f, yRotation + 45f);
        }
        Debug.Log(yRotation);*/

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.transform.localRotation = Quaternion.Euler(0, -yRotation, 0);
        //playerBody.Rotate(Vector3.up * mouseX);

    }
}
