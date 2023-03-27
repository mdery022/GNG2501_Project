using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTimer : MonoBehaviour
{
    [SerializeField]
    private float changeTime;

    [SerializeField]
    private GameObject nextScene;

    void Update()
    {
        changeTime -= Time.deltaTime;

        if (changeTime <= 0)
            nextScene.SetActive(true);
    }
}
