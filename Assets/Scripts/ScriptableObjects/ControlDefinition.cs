using UnityEngine;

/// <summary>
/// A <see cref="ScriptableObject"/> that holds information about a control key and its use.
/// </summary>
[CreateAssetMenu(fileName = "Control", menuName = "ScriptableObjects/Control")]
public class ControlDefinition : ScriptableObject
{
    /// <summary>
    /// The key/input method.
    /// </summary>
    public string Key;

    /// <summary>
    /// What this control does.
    /// </summary>
    public string Usage;
}