using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// A component responsible for keeping track of updating the UI to show resource collection progress.
/// </summary>
public class ResourceTracker : MonoBehaviour
{
    [SerializeField] private MissionCollectResources collectionMission;

    [SerializeField] private ResourceTextSerializedDictionary resourceTextMarkers = new ResourceTextSerializedDictionary();

    [Tooltip("Text that will be formatted to get the result in the UI.\n{0} - Resource Name\n{1} - current resource count\n{2} - desired resource count")]
    [SerializeField] private string formatText;

    /// <summary>
    /// Updates the ui resources.
    /// </summary>
    /// <param name="resources">The resources to update and their new counts.</param>
    public void UpdateResources(Dictionary<Resource, int> resources)
    {
        // loop through each in resources
        foreach (KeyValuePair<Resource, int> pair in resources)
        {
            // pass if not included by this resource tracker
            if (!resourceTextMarkers.ContainsKey(pair.Key))
            {
                continue;
            }

            // get required values
            Resource resource = pair.Key;
            int currentCount = pair.Value;
            int max = collectionMission.GetDesiredResourceCount(pair.Key);

            // set active state of marker to active only if this type is desired
            resourceTextMarkers[pair.Key].gameObject.SetActive(max > 0);

            // set text
            resourceTextMarkers[pair.Key].text = string.Format(formatText, resource.Name, currentCount, max);
        }
    }

    /// <summary>
    /// Sets the <see cref="MissionCollectResources"/> that's being used to track resources.
    /// </summary>
    /// <param name="targetCollectionMission">The mission to track.</param>
    public void SetTargetMission(MissionCollectResources targetCollectionMission)
    {
        collectionMission = targetCollectionMission;

        // get list of all resources tracked in the UI
        Dictionary<Resource, int> initialValues = new Dictionary<Resource, int>();
        foreach (KeyValuePair<Resource, TextMeshProUGUI> pair in resourceTextMarkers)
        {
            initialValues.Add(pair.Key, 0);
        }

        UpdateResources(initialValues);
    }
}