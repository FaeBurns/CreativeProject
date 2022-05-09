using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

/// <summary>
/// A component containing a reference to the components required by the <see cref="HudCompass"/>.
/// </summary>
public class CompassIndicator : MonoBehaviour
{
    /// <summary>
    /// The <see cref="RectTransform"/> used to position the dot.
    /// </summary>
    public RectTransform RectTransform;

    /// <summary>
    /// The <see cref="TextMeshProUGUI"/> used to display the distance.
    /// </summary>
    public TextMeshProUGUI TextMesh;

    /// <summary>
    /// The <see cref="RectTransform"/> used to position the text.
    /// </summary>
    public RectTransform TextRectTransform;
}