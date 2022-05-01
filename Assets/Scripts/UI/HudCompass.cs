using UnityEngine;

/// <summary>
/// Component responsible for updating the compass on the UI.
/// </summary>
public class HudCompass : MonoBehaviour
{
    [SerializeField] private RectTransform directionIndicator;

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
        // LateUpdate as we need camera to rotate before we check it

        // should use cached camera, will do for now
        Camera camera = Camera.main;

        Vector3 targetInLocal = camera.transform.InverseTransformPoint(targetTransform.position);
        float targetAngle = Mathf.Atan2(targetInLocal.x, targetInLocal.z) * Mathf.Rad2Deg;

        // set the position of the indicator to the angle
        directionIndicator.anchoredPosition = new Vector2(targetAngle, 0f);
    }
}