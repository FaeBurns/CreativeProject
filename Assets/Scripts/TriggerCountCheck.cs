using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component responsible for keeping track of how many objects are overlapping with it.
/// </summary>
[RequireComponent(typeof(Collider))]
public class TriggerCountCheck : MonoBehaviour
{
    private readonly Dictionary<GameObject, int> collidingObjects = new Dictionary<GameObject, int>();

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private List<string> tagMask = new List<string>();

    /// <summary>
    /// Gets the amount of objects this trigger is colliding with.
    /// </summary>
    public int OverlapCount => collidingObjects.Count;

    private void OnTriggerEnter(Collider other)
    {
        // check masks
        if (CheckLayerMask(other.gameObject.layer) && tagMask.Contains(other.gameObject.tag))
        {
            // if contained in mapping
            if (collidingObjects.ContainsKey(other.gameObject))
            {
                // increase count
                collidingObjects[other.gameObject]++;
                return;
            }

            // add new
            collidingObjects.Add(other.gameObject, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if key is contained in mapping
        if (collidingObjects.ContainsKey(other.gameObject))
        {
            // decrease count
            collidingObjects[other.gameObject]--;

            // if empty
            if (collidingObjects[other.gameObject] == 0)
            {
                // then remove
                collidingObjects.Remove(other.gameObject);
            }
        }
    }

    private bool CheckLayerMask(int layer)
    {
        // check if mask contains layer
        return layerMask == (layerMask | (1 << layer));
    }
}