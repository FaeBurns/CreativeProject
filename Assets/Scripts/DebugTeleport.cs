using System.Collections;
using UnityEngine;

/// <summary>
/// A component responsible for handling the teleportation of the player to the current mission's compass target.
/// </summary>
public class DebugTeleport : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Mission currentMission = GameManager.Instance.MissionManager.GetCurrentMission();
            if (currentMission == null)
            {
                return;
            }

            Transform compassTarget = currentMission.CompassTarget;

            SwitchableController[] switchables = FindObjectsOfType<SwitchableController>();

            foreach (SwitchableController switchable in switchables)
            {
                if (compassTarget != null)
                {
                    switchable.transform.SetPositionAndRotation(compassTarget.position + new Vector3(0, 2, 0), compassTarget.rotation);
                }
            }
        }
    }
}