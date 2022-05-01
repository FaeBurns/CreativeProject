using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component handling the vehicle's movement.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SwitchableController switchController;
    [SerializeField] private SwitchableController playerSwitchController;

    [SerializeField] private WheelCollider[] allWheels;
    [SerializeField] private WheelCollider[] frontSteeringWheels;
    [SerializeField] private WheelCollider[] backSteeringWheels;

    [SerializeField] private float acceleration = 1000f;
    [SerializeField] private float breakingForce = 800f;
    [SerializeField] private float maxSteerAngle = 7f;
    [SerializeField] private float maxSpeed = 10f;

    [SerializeField] private float exitThreshold = 1.3f;
    [SerializeField] private Transform exitLocation;

    /// <summary>
    /// Freezes all motion.
    /// </summary>
    /// <param name="enable">Should we freeze or unfreeze.</param>
    public void ToggleFreeze(bool enable)
    {
        float brakeForce = enable ? 0 : breakingForce;
        allWheels.Execute(c => c.brakeTorque = brakeForce);

        // always set acceleration to 0
        allWheels.Execute(c => c.motorTorque = 0);

        rb.isKinematic = !enable;
    }

    private void Update()
    {
        // if we are moving slow enough when E is pressed
        if (rb.velocity.magnitude <= exitThreshold && Input.GetKeyDown(KeyCode.E))
        {
            // switch to player
            playerSwitchController.SwitchTo(switchController);

            // teleport player
            playerSwitchController.transform.SetPositionAndRotation(exitLocation.position, exitLocation.rotation);
        }
    }

    private void FixedUpdate()
    {
        // accelerate on input
        float currentAcceleration = acceleration * Input.GetAxis("Vertical");

        // break if space is held
        float currentBrakeForce = Input.GetKey(KeyCode.Space) ? breakingForce : 0;

        // apply acceleration to wheels
        allWheels.Execute(c => c.motorTorque = rb.velocity.sqrMagnitude <= (maxSpeed * maxSpeed) ? currentAcceleration : 0);

        // apply brake to wheels
        allWheels.Execute(c => c.brakeTorque = currentBrakeForce);

        // steer on input
        float currentSteeringAngle = maxSteerAngle * Input.GetAxis("Horizontal");

        // turn specific wheels
        // invert steering of back wheels
        frontSteeringWheels.Execute(c => c.steerAngle = currentSteeringAngle);
        backSteeringWheels.Execute(c => c.steerAngle = -currentSteeringAngle);
    }
}
