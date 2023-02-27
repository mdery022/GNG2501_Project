using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 800.0f;

    [SerializeField]
    new Camera camera;

    [SerializeField]
    Transform body;

    float xRotation = 0.0f, yRotation = 0.0f;

    void Update()
    {
        // To lock the cursor
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void LateUpdate()
    {
        /***
         * CAMERA ROTATION
         */

        yRotation += Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime;
        xRotation -= Input.GetAxis("Mouse Y") * cameraSpeed * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        camera.transform.eulerAngles = new Vector3(xRotation, yRotation, 0f);
        body.eulerAngles = new Vector3(0f, yRotation, 0f);
    }
}