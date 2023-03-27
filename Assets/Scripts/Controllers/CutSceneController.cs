using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    [SerializeField]
    new Camera camera;

    [SerializeField]
    Transform body;

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

        Vector3 forward = camera.transform.forward;
        forward.y = 0;
        body.rotation = Quaternion.LookRotation(forward);
    }
}