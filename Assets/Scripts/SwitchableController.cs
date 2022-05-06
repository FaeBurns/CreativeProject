using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Component for handling the enabling and disabling of components when switching between controlling different objects.
/// </summary>
public class SwitchableController : MonoBehaviour
{
    /// <summary>
    /// Event fired when state is changed.
    /// </summary>
    public UnityEvent<bool> StateChanged;

    [SerializeField] private bool enableOnStart = false;

    [SerializeField] private GameObject[] controlledObjects;
    [SerializeField] private MonoBehaviour[] controlledComponents;

    [SerializeField] private ControlsDisplay controlDisplay;
    [SerializeField] private ControlDefinition[] controls;

    /// <summary>
    /// Gets a value indicating whether this controller is currently active.
    /// </summary>
    public bool CurrentState { get; private set; }

    /// <summary>
    /// Switches control from <paramref name="previous"/> to this.
    /// </summary>
    /// <param name="previous">The <see cref="SwitchableController"/> to disable.</param>
    public void SwitchTo(SwitchableController previous)
    {
        previous.SetState(false);
        SetState(true);
    }

    /// <summary>
    /// Sets the state of all controlled objects.
    /// </summary>
    /// <param name="state">The desired state.</param>
    protected void SetState(bool state)
    {
        CurrentState = state;

        // set enable objects to state
        // set disbale objects to inverse of state
        controlledObjects.Execute(o => o.SetActive(state));
        controlledComponents.Execute(c => c.enabled = state);

        StateChanged?.Invoke(state);

        // if getting enabled, update controls display
        if (state)
        {
            controlDisplay.Refresh(controls);
        }
    }

    private void Start()
    {
        SetState(enableOnStart);
    }
}