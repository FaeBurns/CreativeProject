using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Component responsible for handling the player interacting with objects.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;

    private SwitchableController switchController;

    private void Start()
    {
        switchController = GetComponent<SwitchableController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryEnterVehicle();
        }
    }

    private void TryEnterVehicle()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1))
        {
            SwitchableController target = hitInfo.collider.GetComponentInParent<SwitchableController>();
            if (target != null)
            {
                target.SwitchTo(switchController);
            }
        }
    }
}