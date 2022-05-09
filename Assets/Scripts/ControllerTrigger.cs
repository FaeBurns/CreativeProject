using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A component responsible for notifying other components when an active <see cref="SwitchableController"/> enters its trigger.
/// </summary>
public class ControllerTrigger : MonoBehaviour
{
    /// <summary>
    /// Event that fires when the player enters the trigger.
    /// </summary>
    public UnityEvent Enter;

    /// <summary>
    /// Event that fires when the player exits the trigger.
    /// </summary>
    public UnityEvent Exit;

    private readonly HashSet<SwitchableController> overlapping = new HashSet<SwitchableController>();

    [SerializeField] private bool destroySelfOnEnter;

    private void OnTriggerEnter(Collider other)
    {
        SwitchableController controller = other.GetComponentInParent<SwitchableController>();
        if (controller != null && controller.enabled && !overlapping.Contains(controller))
        {
            overlapping.Add(controller);
            Enter?.Invoke();
            TryDestroy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SwitchableController controller = other.GetComponentInParent<SwitchableController>();
        if (controller != null && controller.enabled && overlapping.Contains(controller))
        {
            Exit?.Invoke();
            overlapping.Remove(controller);
        }
    }

    private void TryDestroy()
    {
        if (destroySelfOnEnter)
        {
            Destroy(gameObject);
        }
    }
}