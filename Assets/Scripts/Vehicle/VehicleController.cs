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

    [SerializeField] private float exitThreshold = 1.3f;
    [SerializeField] private Transform exitLocation;

    private delegate void WheelColliderDelegate(WheelCollider collider);

    /// <summary>
    /// Freezes all motion.
    /// </summary>
    /// <param name="enable">Should we freeze or unfreeze.</param>
    public void ToggleFreeze(bool enable)
    {
        float brakeForce = enable ? 0 : breakingForce;
        RunOnWheelCollection(allWheels, c => c.brakeTorque = brakeForce);

        // always set acceleration to 0
        RunOnWheelCollection(allWheels, c => c.motorTorque = 0);

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
        RunOnWheelCollection(allWheels, c => c.motorTorque = currentAcceleration);

        // apply brake to wheels
        RunOnWheelCollection(allWheels, c => c.brakeTorque = currentBrakeForce);

        // steer on input
        float currentSteeringAngle = maxSteerAngle * Input.GetAxis("Horizontal");

        // turn specific wheels
        // invert steering of back wheels
        RunOnWheelCollection(frontSteeringWheels, c => c.steerAngle = currentSteeringAngle);
        RunOnWheelCollection(backSteeringWheels, c => c.steerAngle = -currentSteeringAngle);
    }

    /// <summary>
    /// Executes a delegate on a <see cref="WheelCollider"/> collection.
    /// </summary>
    /// <param name="collection">The collection to operate on.</param>
    /// <param name="wheelDelegate">The delegate to run on each element in the collection.</param>
    private void RunOnWheelCollection(IEnumerable<WheelCollider> collection, WheelColliderDelegate wheelDelegate)
    {
        foreach (WheelCollider collider in collection)
        {
            wheelDelegate.Invoke(collider);
        }
    }
}
