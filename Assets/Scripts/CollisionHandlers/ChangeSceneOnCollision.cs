using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCollision : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    [SerializeField]
    private string colliderName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == colliderName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
