using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A component responsible for keeping track of progress throughout the mission.
/// </summary>
public class MissionManager : MonoBehaviour
{
    [SerializeField] private Mission currentMission = null;

    [SerializeField] private UnityEvent missionProgressed;

    [SerializeField] private TextMeshProUGUI currentMissionStatement;

    /// <summary>
    /// Checks to see if the mission should be progressed, if so then the mission will be advanced.
    /// </summary>
    public void UpdateMissionProgress()
    {
        if (currentMission.GetProgress() == 1f)
        {
            currentMission = currentMission.GetNextMission();
            missionProgressed?.Invoke();
            currentMission.Initialize(this);
            currentMissionStatement.text = currentMission.MissionStatement;
        }
    }
}