using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected bool isInteractable = true;

    public bool IsInteractable()
    {
        return isInteractable;
    }

    public abstract void OnInteract();
}
