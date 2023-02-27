using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGameOnTimer : MonoBehaviour
{
    [SerializeField]
    private float quitTime;

    void Update()
    {
        quitTime -= Time.deltaTime;

        if (quitTime <= 0)
            Application.Quit();
    }
}
