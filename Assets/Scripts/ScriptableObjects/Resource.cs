using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
}