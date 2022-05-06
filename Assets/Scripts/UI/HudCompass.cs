using TMPro;
using UnityEngine;

/// <summary>
/// Component responsible for updating the compass on the UI.
/// </summary>
public class HudCompass : MonoBehaviour
{
    [SerializeField] private RectTransform directionIndicator;
    [SerializeField] private TextMeshProUGUI distanceText;

    [SerializeField] private Transform targetTransform;

    /// <summary>
    /// Sets a new target for the compass to point towards.
    /// </summary>
    /// <param name="newTarget">The new target to point at.</param>
    public void SetTarget(Transform newTarget)
    {
        targetTransform = newTarget;
    }

    private void LateUpdate()
    {
        // exit if target is not set.
        if (targetTransform == null)
        {
            return;
        }

        // should use cached camera, will do for now
        Camera camera = Camera.main;

        Vector3 targetInLocal = camera.transform.InverseTransformPoint(targetTransform.position);
        float targetAngle = Mathf.Atan2(targetInLocal.x, targetInLocal.z) * Mathf.Rad2Deg;

        // set the position of the indicator to the angle
        directionIndicator.anchoredPosition = new Vector2(targetAngle, 0f);

        // get raw distance
        float distance = Vector3.Distance(camera.transform.position, targetTransform.position);

        // get as integer
        distance = Mathf.Floor(distance);

        // set the distance text
        distanceText.text = distance.ToString() + "m";
    }
}