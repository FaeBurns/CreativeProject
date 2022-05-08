using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A component responsible for showing a resource meter on the UI.
/// </summary>
public class ResourceMeter : MonoBehaviour
{
    [SerializeField] private RectTransform fillBar;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI resourceName;
    [SerializeField] private Resource handlingResource;
    [SerializeField] private float targetHeight = 150f;
    [SerializeField] private float targetWidth = 25f;

    /// <summary>
    /// Refreshes the bar onscreen with the correct size.
    /// </summary>
    /// <param name="resourceCount">The amount of resource to show.</param>
    /// <param name="targetCount">The maximum amount used to calculate how much of the bar is filled.</param>
    public void Refresh(float resourceCount, float targetCount)
    {
        // avoid divide by zero
        if (resourceCount == 0 || targetCount == 0)
        {
            fillBar.sizeDelta = new Vector2(targetHeight, 0);
            return;
        }

        // set the height to the total progress count
        fillBar.sizeDelta = new Vector2(targetWidth, (resourceCount / targetCount) * targetHeight);
    }

    private void Awake()
    {
        iconImage.sprite = handlingResource.Icon;
        fillBar.GetComponent<Image>().color = handlingResource.Color;
        resourceName.text = handlingResource.Name;
    }
}