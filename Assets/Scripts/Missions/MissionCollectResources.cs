using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class MissionCollectResources : Mission
{
    [SerializeField] private ResourceIntSerializedDictionary desiredResourceCount = new ResourceIntSerializedDictionary();
    [SerializeField] private string adaptiveMissionStatement;

    private Dictionary<Resource, int> cachedResources = null;
    private float cachedProgress = 0f;

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
        if (cachedResources is null)
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
            if (cachedResources.TryGetValue(pair.Key, out int value))
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
    /// Event handler responding to <see cref="DepositSpot.ResourceDeposited"/>.
    /// </summary>
    public void OnResourceDeposited(Dictionary<Resource, int> resources)
    {
        cachedResources = resources;
        NotifyOfProgress();
    }
}
