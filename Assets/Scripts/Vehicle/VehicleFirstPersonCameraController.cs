using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Component responsible for controlling the movement of the vehicle's first person camera.
/// </summary>
public class VehicleFirstPersonCameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    [SerializeField] private float horizontalRange = 100f;
    [SerializeField] private float verticalRange = 60f;

    private void OnDisable()
    {
        // reset camera to look forward
        transform.localRotation = Quaternion.identity;
    }

    private void LateUpdate()
    {
        // use scroll wheel to modify sensitivity in place of an options menu
        sensitivity = Mathf.Max(0.5f, sensitivity + (Input.mouseScrollDelta.y / 2));

        // get input values
        float horizontalRotation = Input.GetAxis("Mouse X") * sensitivity;
        float verticalRotation = Input.GetAxis("Mouse Y") * sensitivity;

        // convert from (0-360) to (-180-180)
        float localHorizontalRotation = (transform.localEulerAngles.y > 180) ? transform.localEulerAngles.y - 360 : transform.localEulerAngles.y;
        float localVerticalRotation = (transform.localEulerAngles.x > 180) ? transform.localEulerAngles.x - 360 : transform.localEulerAngles.x;

        // get final values and clamp them
        float newHorizontalAngle = Mathf.Clamp(localHorizontalRotation + horizontalRotation, -horizontalRange, horizontalRange);
        float newVerticalAngle = Mathf.Clamp(localVerticalRotation - verticalRotation, -verticalRange, verticalRange);

        // set rotation of camera
        transform.localRotation = Quaternion.Euler(newVerticalAngle, newHorizontalAngle, 0);
    }
}