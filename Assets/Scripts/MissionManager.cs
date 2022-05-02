using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A component responsible for keeping track of progress throughout the mission.
/// </summary>
public class MissionManager : MonoBehaviour
{
    [SerializeField] private ResourceIntSerializedDictionary desiredResourceCount = new ResourceIntSerializedDictionary();

    /// <summary>
    /// Gets the desired count of a type of resource.
    /// </summary>
    /// <param name="resource">The resource to check against.</param>
    /// <returns>Resource count if the resource is found, 0 if not.</returns>
    public int GetDesiredResourceCount(Resource resource)
    {
        if (desiredResourceCount.TryGetValue(resource, out int result))
        {
            return result;
        }

        return 0;
    }
}