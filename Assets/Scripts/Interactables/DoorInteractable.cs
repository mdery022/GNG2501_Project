using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    [SerializeField]
    private float openAngel, movementSpeed;

    [SerializeField]
    private AudioClip openClip, closeClip;

    [SerializeField]
    private AudioSource audioSource;

    private DoorState doorState = DoorState.Closed;
    private Vector3 initialPosition;

    public override void OnInteract()
    {
        if (doorState != DoorState.Closing && doorState != DoorState.Opening)
        {
            audioSource.PlayOneShot(doorState == DoorState.Closed ? openClip : closeClip);
            doorState = doorState == DoorState.Closed ? DoorState.Opening : DoorState.Closing;
        }
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {

        Vector3 currentEulerAngle = transform.parent.rotation.eulerAngles;

        if (doorState == DoorState.Opening)
        {
            transform.parent.rotation = Quaternion.Euler(new Vector3(0.0f, movementSpeed * Time.deltaTime + currentEulerAngle.y, 0.0f));

            if (transform.parent.rotation.eulerAngles.y > (initialPosition.y + openAngel))
            {
                doorState = DoorState.Open;
            }
        }
        else if (doorState == DoorState.Closing)
        {
            transform.parent.rotation = Quaternion.Euler(new Vector3(0.0f, currentEulerAngle.y - movementSpeed * Time.deltaTime, 0.0f));

            currentEulerAngle = transform.parent.rotation.eulerAngles;

            if (currentEulerAngle.y > 350) currentEulerAngle.y = 0.0f;

            if (currentEulerAngle.y <= initialPosition.y)
            {
                doorState = DoorState.Closed;
            }
        }
    }
}

public enum DoorState
{
    Open,
    Closed,
    Opening,
    Closing
}
