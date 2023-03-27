using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 2.0f, raycastDistance = 5.0f;

    [SerializeField]
    new Camera camera;

    [SerializeField]
    GameObject interactText;

    [SerializeField]
    private string colliderName;

    [SerializeField]
    private SteamVR_Action_Vector2 moveAction;

    [SerializeField]
    private SteamVR_Action_Boolean useAction;

    [SerializeField]
    Transform body;

    float horizontalAxis, verticalAxis;
    private Interactable interactable;
    private Animator animator;

    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        interactText.SetActive(false);
    }

    void Update()
    {
        // To lock the cursor
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }

        // Set the interactable text visible when looking at interactable object
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, raycastDistance))
        {
            interactable = hit.transform.gameObject.GetComponent<Interactable>();

            if (interactable != null && interactable.IsInteractable())
            {
                interactText.SetActive(true);
            }
            else
            {
                interactText.SetActive(false);
            }
        } else
        {
            interactText.SetActive(false);
        }

        /***
         * CHARACTER MOVEMENT
         */

        Vector2 movement = moveAction.axis;

        horizontalAxis = movement.x;
        verticalAxis = movement.y;

        // Get the direction of the camera
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 direction = forward * verticalAxis + right * horizontalAxis;

        // Move the player according to the inputs
        transform.Translate(direction * movementSpeed * Time.deltaTime);

        // Set the animation to walk if the character is moving
        SetWalking(horizontalAxis, verticalAxis);

        /***
         * PLAYER INTERACTIONS
         */

        if (useAction.stateDown)
        {
            if (interactable != null)
            {
                interactable.OnInteract();
            }
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

    private void SetWalking(float horizontalAxis, float verticalAxis)
    {
        /*float verticalAxis = (forward * movement.z).z;
        float horizontalAxis = (forward * movement.x).x;*/
        float treshold = 0.0f;

        // Going forward
        if (verticalAxis > treshold)
        {
            // Going forward to the right
            if (horizontalAxis > treshold) animator.SetInteger("State", 2);
            // Going forward to the left
            else if (horizontalAxis < -treshold) animator.SetInteger("State", 8);
            else animator.SetInteger("State", 1);
        }
        // Going backward
        else if (verticalAxis < -treshold)
        {
            // Going backward to the right
            if (horizontalAxis > treshold) animator.SetInteger("State", 4);
            // Going backward to the left
            else if (horizontalAxis < -treshold) animator.SetInteger("State", 6);
            else animator.SetInteger("State", 5);
        }
        else
        {
            // Going to the right
            if (horizontalAxis > treshold) animator.SetInteger("State", 3);
            // Going to the left
            else if (horizontalAxis < -treshold) animator.SetInteger("State", 7);
            else animator.SetInteger("State", 0);
        }
    }
}

public enum PlayerState
{
    Idle,
    Walk
}