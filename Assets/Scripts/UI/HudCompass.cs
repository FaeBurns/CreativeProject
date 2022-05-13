using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Component responsible for updating the compass on the UI.
/// </summary>
public class HudCompass : MonoBehaviour
{
    [SerializeField] private float rotationalRange = 330f;
    [SerializeField] private float textOffset = 24f;

    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private List<CompassIndicator> indicators = new List<CompassIndicator>();
    [SerializeField] private List<Transform> targets;

    private RectTransform rectTransform;

    /// <summary>
    /// Adds a new target to the collection.
    /// </summary>
    /// <param name="newTarget">The new target to point at.</param>
    public void AddTarget(Transform newTarget)
    {
        targets.Add(newTarget);
        UpdatePointCount();
    }

    /// <summary>
    /// Removes a target from the collection.
    /// </summary>
    /// <param name="target">The target to remove.</param>
    public void RemoveTarget(Transform target)
    {
        targets.Remove(target);
        UpdatePointCount();
    }

    /// <summary>
    /// Checks if a target is in the collection.
    /// </summary>
    /// <param name="target">The target to check.</param>
    /// <returns>True if a target was found, false if not.</returns>
    public bool ContainsTarget(Transform target)
    {
        return targets.Contains(target);
    }

    /// <summary>
    /// Clears all targets from the collection.
    /// </summary>
    public void ClearTargets()
    {
        targets.Clear();
        UpdatePointCount();
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // exit if target is not set.
        if (targets == null)
        {
            return;
        }

        // should use cached camera, will do for now
        Camera camera = Camera.main;

        for (int i = 0; i < targets.Count; i++)
        {
            // Indicators wil always be the same size as targets
            UpdateCompassPoint(targets[i], indicators[i], i, camera);
        }
    }

    private void UpdateCompassPoint(Transform target, CompassIndicator indicator, int index, Camera camera)
    {
        Vector3 targetInLocal = camera.transform.InverseTransformPoint(target.position);
        float targetAngle = Mathf.Atan2(targetInLocal.x, targetInLocal.z) * Mathf.Rad2Deg;

        float multiplier = rectTransform.sizeDelta.x / rotationalRange;

        // set the position of the indicator to the angle
        indicator.RectTransform.anchoredPosition = new Vector2(targetAngle * multiplier, 0f);

        // get raw distance
        float distance = Vector3.Distance(camera.transform.position, target.position);

        // get as integer
        distance = Mathf.Floor(distance);

        // set the distance text
        indicator.TextMesh.text = distance.ToString() + "m";
        indicator.TextRectTransform.anchoredPosition = new Vector2(0, (index + 1) * textOffset);
    }

    private void UpdatePointCount()
    {
        int requiredCount = targets.Count;

        // add more
        // loop while current count is less than required count
        while (indicators.Count < requiredCount)
        {
            // spawn new indicator
            CompassIndicator newIndicator = Instantiate(indicatorPrefab, rectTransform).GetComponent<CompassIndicator>();

            // add new to list
            indicators.Add(newIndicator);
        }

        // remove excess
        // loop while current count is greater than required count
        while (indicators.Count > requiredCount)
        {
            // always remove first - stops potential issues
            // destroy object before removing it
            Destroy(indicators[0].gameObject);
            indicators.RemoveAt(0);
        }
    }
}