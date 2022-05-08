using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component handling the vehicle's movement.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour, IVitalityChecker
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

    [SerializeField] private VehicleFirstPersonCameraController forwardCam;
    [SerializeField] private VehicleFirstPersonCameraController backCam;

    [SerializeField] private float fuelUsageMultiplier = 0.01f;

    private bool currentCamForward;

    private bool canMove = true;
    private float movementMultiplier = 1f;

    private float currentAcceleration = 0f;

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

        // reset camera and controls.
        currentCamForward = true;
        movementMultiplier = 1;
    }

    /// <summary>
    /// Handles <see cref="ResourceVitality.fullyDrained"/>.
    /// </summary>
    public void OnDrained()
    {
        canMove = false;
    }

    /// <summary>
    /// Handles <see cref="ResourceVitality.recovered"/>.
    /// </summary>
    public void OnRecovered()
    {
        canMove = true;
    }

    /// <inheritdoc/>
    public float GetCurrentDrain()
    {
        /*
        float currentSpeed = rb.velocity.magnitude;

        // get the difference between speeds
        // make sure it does not go negative
        float diff = Mathf.Max(currentSpeed - previousSpeed, 0);

        // only actually count if acceleration is intentional
        diff *= acceleration > 0 ? 1 : 0;

        // update previous speed
        previousSpeed = currentSpeed;

        return diff;
        */
        return (Mathf.Abs(currentAcceleration) * fuelUsageMultiplier) * Time.deltaTime;
    }

    private void Update()
    {
        // if we are moving slow enough when E is pressed
        if (rb.velocity.sqrMagnitude <= (exitThreshold * exitThreshold) && Input.GetKeyDown(KeyCode.E))
        {
            // switch to player
            playerSwitchController.SwitchTo(switchController);

            // teleport player
            playerSwitchController.transform.SetPositionAndRotation(exitLocation.position, exitLocation.rotation);

            return;
        }

        // if Q has been pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // switch cameras
            backCam.gameObject.SetActive(currentCamForward);
            forwardCam.gameObject.SetActive(!currentCamForward);

            // invert controls
            movementMultiplier *= -1;

            // set state for next camera switch
            currentCamForward = !currentCamForward;
        }
    }

    private void FixedUpdate()
    {
        // accelerate on input
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        // if we can't move
        if (!canMove)
        {
            // set acceleration to 0
            currentAcceleration = 0;
        }

        // break if space is held
        float currentBrakeForce = Input.GetKey(KeyCode.Space) ? breakingForce : 0;

        // apply acceleration to wheels
        allWheels.Execute(c => c.motorTorque = rb.velocity.sqrMagnitude <= (maxSpeed * maxSpeed) ? currentAcceleration * movementMultiplier : 0);

        // apply brake to wheels
        allWheels.Execute(c => c.brakeTorque = currentBrakeForce * movementMultiplier);

        // steer on input
        float currentSteeringAngle = maxSteerAngle * Input.GetAxis("Horizontal");

        // turn specific wheels
        // invert steering of back wheels
        frontSteeringWheels.Execute(c => c.steerAngle = currentSteeringAngle * movementMultiplier);
        backSteeringWheels.Execute(c => c.steerAngle = -currentSteeringAngle * movementMultiplier);
    }
}
