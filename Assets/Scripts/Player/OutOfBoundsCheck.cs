using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A component responsible for checking if the player has gone out of bounds.
/// </summary>
public class OutOfBoundsCheck : MonoBehaviour
{
    [SerializeField] private SwitchableController vehicleController;
    [SerializeField] private float maxDepth = -1000f;

    private void FixedUpdate()
    {
        // OOB check
        if (transform.position.y < maxDepth)
        {
            // switch from this to vehicle
            vehicleController.SwitchTo(GetComponent<SwitchableController>());

            // teleport somewhere safe
            transform.position = Vector3.zero;
        }
    }
}
