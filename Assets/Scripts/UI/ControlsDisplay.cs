using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ControlsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject controlsPrefab;
    private List<ControlsAccessor> activeControls = new List<ControlsAccessor>();

    public void Refresh(ControlDefinition[] controls)
    {
        MatchCount(controls.Length);

        for (int i = 0; i < controls.Length; i++)
        {
            activeControls[i].keyTextMesh.text = controls[i].Key;
            activeControls[i].useTextMesh.text = controls[i].Usage;
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
        for (int t = count; t < activeControls.Count; t++)
        {
            // destroy extras
            Destroy(activeControls[t].gameObject);
            activeControls.RemoveAt(t);
        }
    }
}