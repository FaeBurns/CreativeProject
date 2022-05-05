using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component responsible for checking if the vehicle has flipped over.
/// </summary>
public class FlipChecker : MonoBehaviour
{
    [SerializeField] private Mission vehicleFlippedMission;
    [SerializeField] private float timeToCheckFor = 3f;

    private float timeAtStart = 0f;
    private HashSet<GameObject> collided = new HashSet<GameObject>();

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
        if (!collided.Contains(other.gameObject))
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
        if (collided.Contains(other.gameObject))
        {
            collided.Remove(other.gameObject);
        }
    }
}