using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject nextScene;

    [SerializeField]
    private string colliderName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == colliderName)
        {
            nextScene.SetActive(true);
        }
    }
}
