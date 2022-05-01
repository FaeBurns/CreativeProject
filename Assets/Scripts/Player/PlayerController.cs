using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component responsible for allowing the player to move.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerCameraController))]
[RequireComponent(typeof(SwitchableController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform rayOrigin;

    [SerializeField]
    private float speed = 0.1f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // get movement direction
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // get non-smoothed input
        Vector2 rawAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // if there is any input
        if (rawAxis.magnitude > 0)
        {
            // normalize the input vector
            // this removes the rampup from the input and makes movement snappier
            moveDirection.Normalize();
        }

        // rotate movement direction to point forwards relative to the player
        moveDirection = Quaternion.LookRotation(transform.forward, transform.up) * moveDirection * speed;

        // fire a ray downwards
        Ray ray = new Ray(rayOrigin.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
        {
            // modify move direction to use the normal of the hit object as its plane of movement
            moveDirection = Vector3.ProjectOnPlane(moveDirection, hitInfo.normal);
        }

        // move the rigidbody
        rb.MovePosition(rb.position + moveDirection);
    }
}