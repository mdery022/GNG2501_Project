using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 2.0f, raycastDistance = 5.0f, cameraSpeed = 800.0f;

    [SerializeField]
    new Camera camera;

    [SerializeField]
    GameObject interactText;

    [SerializeField]
    Transform body;

    float horizontalAxis, verticalAxis;
    float xRotation = 0.0f, yRotation = 0.0f;
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

        /* TODO: SITTING MAYBE?
        if (state == PlayerState.Sit)
        {
            // TODO: animator.SetInteger("State", 2);

            if (Input.GetKeyDown(KeyCode.E))
            {
                state = PlayerState.Idle;
            }
            else
            {
                return;
            }
        }*/

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

        // Get the keys pressed by the player (A, W, S, D)
        horizontalAxis = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);
        verticalAxis = (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0);

        // Get the firection of the camera
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

        if (Input.GetKeyDown(KeyCode.E))
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

        yRotation += Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime;
        xRotation -= Input.GetAxis("Mouse Y") * cameraSpeed * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        camera.transform.eulerAngles = new Vector3(xRotation, yRotation, 0f);
        body.eulerAngles = new Vector3(0f, yRotation, 0f);
    }

    /* TODO: MAYBE SIT
    public void SetSitting(Vector3 position)
    {
        transform.position = position;
        state = PlayerState.Sit;
    }*/

    private void SetWalking(float horizontalAxis, float verticalAxis)
    {
        // Going forward
        if (verticalAxis > 0)
        {
            // Going forward to the right
            if (horizontalAxis > 0) animator.SetInteger("State", 2);
            // Going forward to the left
            else if (horizontalAxis < 0) animator.SetInteger("State", 8);
            else animator.SetInteger("State", 1);
        }
        // Going backward
        else if (verticalAxis < 0)
        {
            // Going backward to the right
            if (horizontalAxis > 0) animator.SetInteger("State", 4);
            // Going backward to the left
            else if (horizontalAxis < 0) animator.SetInteger("State", 6);
            else animator.SetInteger("State", 5);
        }
        else
        {
            // Going to the right
            if (horizontalAxis > 0) animator.SetInteger("State", 3);
            // Going to the left
            else if (horizontalAxis < 0) animator.SetInteger("State", 7);
            else animator.SetInteger("State", 0);
        }
    }
}

public enum PlayerState
{
    Idle,
    Walk
}