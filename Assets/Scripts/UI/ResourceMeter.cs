using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMeter : MonoBehaviour
{
    [SerializeField] private RectTransform fillBar;
    [SerializeField] private Image iconImage;
    [SerializeField] private Resource handlingResource;
    [SerializeField] private float targetHeight = 150f;
    [SerializeField] private float targetWidth = 25f;

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
    }
}