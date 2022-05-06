using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// A component responsible for updating the Controls UI.
/// </summary>
public class ControlsDisplay : MonoBehaviour
{
    private readonly List<ControlsAccessor> activeControls = new List<ControlsAccessor>();

    [SerializeField] private GameObject controlsPrefab;

    /// <summary>
    /// Refreshes the UI with a new set of controls.
    /// </summary>
    /// <param name="controls">The controls to push to the UI.</param>
    public void Refresh(ControlDefinition[] controls)
    {
        MatchCount(controls.Length);

        for (int i = 0; i < controls.Length; i++)
        {
            activeControls[i].KeyTextMesh.text = controls[i].Key;
            activeControls[i].UseTextMesh.text = controls[i].Usage;
        }
    }

    private void MatchCount(int count)
    {
        // add any extra
        for (int i = activeControls.Count; i < count; i++)
        {
            // add new
            activeControls.Add(Instantiate(controlsPrefab, transform).GetComponent<ControlsAccessor>());
        }

        // remove any unrequired
        // loop x times where x is how much higher activeControls.Count is than count
        while (count < activeControls.Count)
        {
            // always destroy first element - stops issues with array size
            Destroy(activeControls[0].gameObject);
            activeControls.RemoveAt(0);
        }
    }
}