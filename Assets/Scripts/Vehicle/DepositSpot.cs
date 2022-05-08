﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Component responsible for keeping track of deposited items.
/// </summary>
public class DepositSpot : MonoBehaviour
{
    /// <summary>
    /// Event fired when a resource is deposited.
    /// Contains dictionary of all resource counts.
    /// </summary>
    public UnityEvent<Dictionary<Resource, int>> ResourceDeposited;

    /// <summary>
    /// Event fired when a resource is deposited.
    /// Only contains the new resource type.
    /// </summary>
    public UnityEvent<Resource> SingularResourceDeposited;

    private readonly Dictionary<Resource, int> resourceCount = new Dictionary<Resource, int>();

    /// <summary>
    /// Adds one to the resource count for this resource.
    /// </summary>
    /// <param name="resource">The resource to count.</param>
    public void Deposit(Resource resource)
    {
        // check if a resource of that type is already present in the dictionary
        if (resourceCount.ContainsKey(resource))
        {
            // if so then increase the count
            resourceCount[resource]++;
        }
        else
        {
            // if not then add a new entry with a count of one
            resourceCount.Add(resource, 1);
        }

        // invoke notify event
        ResourceDeposited?.Invoke(resourceCount);
        SingularResourceDeposited?.Invoke(resource);

        Debug.LogFormat("Resource deposited: {0} x{1}", resource.name, resourceCount[resource]);
    }

    /// <summary>
    /// Clears everything inside this deposit spot.
    /// </summary>
    public void ResetContainer()
    {
        resourceCount.Clear();
    }
}
