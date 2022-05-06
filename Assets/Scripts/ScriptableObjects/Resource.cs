using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A <see cref="ScriptableObject"/> responsible for keeping track of information about a resource.
/// </summary>
[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resource")]
public class Resource : ScriptableObject
{
    /// <summary>
    /// The name of this resource.
    /// </summary>
    public string Name;

    /// <summary>
    /// The icon this resource uses.
    /// </summary>
    public Sprite Icon;

    /// <summary>
    /// The color this resource uses.
    /// </summary>
    public Color Color;

    /// <summary>
    /// How much Vitality is stored in this resource.
    /// </summary>
    public float StoredVitality;
}