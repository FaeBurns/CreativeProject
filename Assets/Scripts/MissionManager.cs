﻿using System;
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

    [SerializeField] private UnityEvent missionBegun;

    [SerializeField] private TextMeshProUGUI currentMissionStatement;

    /// <summary>
    /// Checks to see if the mission should be progressed, if so then the mission will be advanced.
    /// </summary>
    public void UpdateMissionProgress()
    {
        if (currentMission == null)
        {
            currentMissionStatement.text = "No mission selected";
            return;
        }

        if (currentMission.GetProgress() >= 1f)
        {
            // end current mission
            currentMission.End();

            // start next mission
            currentMission = currentMission.GetNextMission();
            StartNewMission();
            return;
        }

        // update statement text regardless - may display progress
        currentMissionStatement.text = currentMission.MissionStatement;
    }

    /// <summary>
    /// Forces the MissionManager to start a new mission.
    /// </summary>
    /// <param name="mission">The new mission to start.</param>
    public void ForceNewMission(Mission mission)
    {
        currentMission.ForceCancel();
        currentMission = mission;
        StartNewMission();
    }

    /// <summary>
    /// Gets the current active mission.
    /// </summary>
    /// <returns>the current active mission.</returns>
    public Mission GetCurrentMission()
    {
        return currentMission;
    }

    private void Start()
    {
        StartNewMission();
    }

    private void StartNewMission()
    {
        if (currentMission != null)
        {
            missionBegun?.Invoke();
            currentMission.Initialize(this);
            currentMissionStatement.text = currentMission.MissionStatement;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (currentMission != null)
            {
                currentMission.ForceCancel();
                currentMission = currentMission.GetNextMission();
                StartNewMission();
            }
        }
    }
}