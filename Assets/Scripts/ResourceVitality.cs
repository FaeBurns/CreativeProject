using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A component responsible for keeping track of a resource and its drain over time.
/// </summary>
public class ResourceVitality : MonoBehaviour
{
    /// <summary>
    /// The amount of the resource tracked here.
    /// </summary>
    public float Vitality = 0f;

    /// <summary>
    /// The target count of the resource tracked here.
    /// Vitality can go above this value but any higher values will not be shown properly.
    /// </summary>
    public float TargetVitality = 100f;

    [SerializeField] private ResourceMeter meterTarget;
    [SerializeField] private Component vitalityTarget;
    [SerializeField] private Resource resource;

    [SerializeField] private UnityEvent fullyDrained;
    [SerializeField] private UnityEvent recovered;

    private IVitalityChecker cachedTarget;

    /// <summary>
    /// Gets a value indicating whether the resource is drained.
    /// </summary>
    [field: SerializeField]
    public bool Drained { get; private set; }

    /// <summary>
    /// On Resource Deposited.
    /// </summary>
    /// <param name="deposited">The resource deposited.</param>
    public void OnDeposit(Resource deposited)
    {
        // if the deposited resources is equal to the target resource
        if (deposited == resource)
        {
            Vitality += resource.StoredVitality;
            Drained = false;
            recovered?.Invoke();
        }
    }

    /// <summary>
    /// Updates <see cref="Vitality"/> and checks to see if it is drained.
    /// </summary>
    public void Refresh()
    {
        Vitality -= cachedTarget.GetCurrentDrain();

        // if drained, exit early
        if (Drained)
        {
            return;
        }

        // update UI
        if (meterTarget != null)
        {
            meterTarget.Refresh(Vitality, TargetVitality);
        }

        if (Vitality <= 0)
        {
            Drained = true;
            fullyDrained?.Invoke();
        }
    }

    private void Awake()
    {
        cachedTarget = vitalityTarget as IVitalityChecker;
        if (cachedTarget == null)
        {
            Debug.LogError($"{vitalityTarget.GetType()} does not implement {nameof(IVitalityChecker)}");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Vitality = TargetVitality;
        }

        Refresh();
    }
}