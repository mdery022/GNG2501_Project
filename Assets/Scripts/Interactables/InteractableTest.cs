using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTest : Interactable
{
    [SerializeField]
    string text;

    public override void OnInteract()
    {
        Debug.Log(text);
    }
}
