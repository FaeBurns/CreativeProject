using System.Collections;
using UnityEngine;

public class DebugTeleport : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Mission currentMission = GameManager.Instance.MissionManager.GetCurrentMission();
            if(currentMission == null)
            {
                return;
            }

            Transform compassTarget = currentMission.CompassTarget;

            SwitchableController[] switchables = FindObjectsOfType<SwitchableController>();

            foreach(SwitchableController switchable in switchables)
            {
                if (compassTarget != null) 
                {
                    switchable.transform.SetPositionAndRotation(compassTarget.position + new Vector3(0, 2, 0), compassTarget.rotation);
                }
            }
        }
    }
}