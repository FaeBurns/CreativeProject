using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component responsible for rotating the player's camera.
/// </summary>
public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private Transform capsuleTransform;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float sensitivity;

    private Rigidbody rb;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        // use scroll wheel to modify sensitivity in place of an options menu
        sensitivity = Mathf.Max(0.5f, sensitivity + Input.mouseScrollDelta.y);

        // get input values
        float horizontalRotation = Input.GetAxis("Mouse X") * sensitivity;
        float verticalRotation = Input.GetAxis("Mouse Y") * sensitivity;

        // set rotation of capsule and camera
        // rb.rotation has to be used here or it cancels movement
        rb.rotation = Quaternion.Euler(0, rb.rotation.eulerAngles.y + horizontalRotation, 0);
        cameraTransform.localRotation = Quaternion.Euler(cameraTransform.eulerAngles.x - verticalRotation, 0, 0);
    }
}