using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// A component responsible for holding two <see cref="TextMeshProUGUI"/> instances for use in the Controls UI Display.
/// </summary>
public class ControlsAccessor : MonoBehaviour
{
    /// <summary>
    /// The Text Mesh that will show the key.
    /// </summary>
    public TextMeshProUGUI KeyTextMesh;

    /// <summary>
    /// The Text Mesh that will show the usage.
    /// </summary>
    public TextMeshProUGUI UseTextMesh;
}