﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A mission responsible with handling the collection of resources.
/// </summary>
public class MissionCollectResources : Mission
{
    private readonly Dictionary<Resource, int> depositedResources = new Dictionary<Resource, int>();

    [SerializeField] private ResourceIntSerializedDictionary desiredResourceCount = new ResourceIntSerializedDictionary();
    [SerializeField] private string adaptiveMissionStatement;

    private float cachedProgress = 0f;

    /// <inheritdoc/>
    public override string MissionStatement => string.Format(adaptiveMissionStatement, Mathf.Floor(cachedProgress * 100f));

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

    /// <inheritdoc/>
    public override float GetProgress()
    {
        if (depositedResources is null)
        {
            return 0;
        }

        // initialize values
        int totalResourceCount = 0;
        int totalResourcesCollected = 0;

        // loop through all desired resources
        foreach (KeyValuePair<Resource, int> pair in desiredResourceCount)
        {
            // add to total count
            totalResourceCount += pair.Value;

            // if the input resources has an entry for it
            if (depositedResources.TryGetValue(pair.Key, out int value))
            {
                // add to the tally of collected resources
                totalResourcesCollected += value;
            }
        }

        // avoid divide by zero.
        if (totalResourceCount == 0 || totalResourcesCollected == 0)
        {
            return 0;
        }

        // return percentage of collected resources
        // convert one of them to float before dividing
        cachedProgress = (float)totalResourcesCollected / totalResourceCount;
        return cachedProgress;
    }

    /// <summary>
    /// Event handler responding to <see cref="DepositSpot.SingularResourceDeposited"/>.
    /// </summary>
    /// <param name="resource">The resource being deposited.</param>
    public void OnResourceDeposited(Resource resource)
    {
        if (Host != null)
        {
            if (depositedResources.ContainsKey(resource))
            {
                depositedResources[resource]++;
            }
            else
            {
                depositedResources.Add(resource, 1);
            }

            GameManager.Instance.ResourceTracker.UpdateResources(depositedResources);

            NotifyOfProgress();
        }
    }

    /// <inheritdoc/>
    protected override void Begin()
    {
        GameManager.Instance.ResourceTracker.SetTargetMission(this);
        GameManager.Instance.ResourceTracker.gameObject.SetActive(true);
        GameManager.Instance.ResourceTracker.UpdateResources(depositedResources);
    }

    /// <inheritdoc/>
    protected override void CommonClose()
    {
        GameManager.Instance.ResourceTracker.gameObject.SetActive(false);
    }
}
