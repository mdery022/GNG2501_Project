using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour
{
    Camera mainCamera;
    Vector3 playerDirection;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = (mainCamera.transform.position - transform.position);
        playerDirection.x = -playerDirection.x;
        transform.LookAt(transform.position + playerDirection);
    }
}
