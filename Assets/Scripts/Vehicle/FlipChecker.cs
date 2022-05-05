using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component responsible for checking if the vehicle has flipped over.
/// </summary>
public class FlipChecker : MonoBehaviour
{
    private readonly HashSet<GameObject> collided = new HashSet<GameObject>();

    [SerializeField] private Mission vehicleFlippedMission;
    [SerializeField] private float timeToCheckFor = 3f;
    [SerializeField] private string targetTag = "Terrain";

    private float timeAtStart = 0f;

    private void Update()
    {
        // if there is at least one collision
        // and timeToCheckFor seconds have passes since that first collision
        if (collided.Count > 0 && timeAtStart + timeToCheckFor < Time.time)
        {
            // notify Player of failure
            GameManager.Instance.MissionManager.ForceNewMission(vehicleFlippedMission);

            // disable this script
            enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if other has already been seen - otherwise may cause issues if other object has multiple colliders
        if (!collided.Contains(other.gameObject) && other.gameObject.CompareTag(targetTag))
        {
            collided.Add(other.gameObject);
        }

        if (collided.Count == 1)
        {
            timeAtStart = Time.time;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // check if other has already been seen - otherwise may cause issues if other object has multiple colliders
        // do not need to check for tag as a non-tagged object would not make it into the set
        if (collided.Contains(other.gameObject))
        {
            collided.Remove(other.gameObject);
        }
    }
}