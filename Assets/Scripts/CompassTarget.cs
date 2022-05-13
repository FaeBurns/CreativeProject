using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component containing information about a compass target.
/// </summary>
public class CompassTarget : MonoBehaviour
{
    /// <summary>
    /// The colour to apply to the compass point.
    /// </summary>
    public Color TargetColor;

    private void OnValidate()
    {
        // forces alpha to always visible
        TargetColor.a = 1f;
    }
}
