using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An abstract component responsible for keeping track of a missions progress.
/// The <see cref="Begin"/> method will be called when this mission is started.
/// </summary>
public abstract class Mission : MonoBehaviour
{
    [SerializeField] private Mission defaultNextMission;
    [SerializeField] private HudCompass compass;
    [SerializeField] private Transform compassTarget;

    [SerializeField] private UnityEvent started;
    [SerializeField] private UnityEvent completed;

    /// <summary>
    /// Gets or Sets the compass target ascociated with this mission.
    /// </summary>
    public Transform CompassTarget { get => compassTarget; set => compassTarget = value; }

    /// <summary>
    /// Gets the mission statement ascociated with this mission.
    /// </summary>
    public abstract string MissionStatement { get; }

    /// <summary>
    /// Gets a value indicating whether this mission has been cancelled.
    /// </summary>
    protected bool WasCancelled { get; private set; } = false;

    /// <summary>
    /// Gets the MissionManager this mission is hosted by.
    /// </summary>
    protected MissionManager Host { get; private set; }

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

        if (compass != null)
        {
            compass.SetTarget(compassTarget);
        }

        Begin();
        started?.Invoke();
    }

    /// <summary>
    /// Ends this mission.
    /// </summary>
    public void End()
    {
        Finish();
        completed?.Invoke();
    }

    /// <summary>
    /// Forcibly cancels this mission.
    /// </summary>
    public void ForceCancel()
    {
        WasCancelled = true;
        Cancel();
    }

    /// <summary>
    /// Notifies the host of mission progress.
    /// </summary>
    public void NotifyOfProgress()
    {
        if (!WasCancelled)
        {
            Host.UpdateMissionProgress();
            return;
        }

        End();
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

    /// <summary>
    /// Called when this Mission has ended.
    /// </summary>
    protected virtual void Finish()
    {
    }

    /// <summary>
    /// Called when this Mission is forcibly cancelled.
    /// Finish is not called alongside it.
    /// </summary>
    protected virtual void Cancel()
    {
    }
}
