using System.Collections;
using UnityEngine;

/// <summary>
/// An interface responsible for common functionality required by a <see cref="ResourceVitality"/> component.
/// </summary>
public interface IVitalityChecker
{
    /// <summary>
    /// Gets the drain on the vitality.
    /// </summary>
    /// <returns>How much to drain from the resources vitality.</returns>
    public float GetCurrentDrain();
}