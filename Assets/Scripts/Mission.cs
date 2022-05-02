using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// An abstract component responsible for keeping track of a missions progress.
/// The <see cref="Begin"/> method will be called when this mission is started.
/// </summary>
public abstract class Mission : MonoBehaviour
{
    [SerializeField] private string missionStatement;
    [SerializeField] private Mission defaultNextMission;

    /// <summary>
    /// Gets the mission statement ascociated with this mission.
    /// </summary>
    public virtual string MissionStatement { get; }

    private MissionManager Host { get; set; }

    /// <summary>
    /// Gets, on a scale of 0 to 1, how far along the progress of this mission is. A value of 1 will result in the mission being advanced.
    /// </summary>
    /// <returns>How far along this mission is.</returns>
    public abstract float GetProgress();

    /// <summary>
    /// Initializes this mission when it is set as current.
    /// </summary>
    /// <param name="host">The <see cref="MissionManager"/> hosting this mission.</param>
    public void Initialize(MissionManager host)
    {
        Host = host;
    }

    /// <summary>
    /// Notifies the host of mission progress.
    /// </summary>
    public void NotifyOfProgress()
    {
        Host.UpdateMissionProgress();
    }

    /// <summary>
    /// Gets the next mission based on context in this mission.
    /// </summary>
    /// <returns>The new mission to use.</returns>
    public virtual Mission GetNextMission()
    {
        return defaultNextMission;
    }

    /// <summary>
    /// Called when this Mission is started.
    /// </summary>
    protected virtual void Begin()
    {
    }
}
