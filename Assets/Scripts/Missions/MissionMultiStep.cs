using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A <see cref="Mission"/> responsible for tracking the progress of multiple sub-missions.
/// </summary>
public class MissionMultiStep : Mission
{
    [SerializeField] private string missionStatementFormat;
    [SerializeField] private List<Mission> subMissions = new List<Mission>();

    /// <inheritdoc/>
    public override string MissionStatement => string.Format(missionStatementFormat.Replace("\\n", "\n"), GetTotalMissionsCompleted(), subMissions.Count);

    /// <inheritdoc/>
    public override float GetProgress()
    {
        return GetTotalMissionsCompleted() / subMissions.Count;
    }

    /// <inheritdoc/>
    protected override void Begin()
    {
        foreach (Mission mission in subMissions)
        {
            // remove target
            // if the target is still required then it will just be added back later
            GameManager.Instance.HudCompass.RemoveTarget(mission.transform);

            // if mission is not complete
            if (mission.GetProgress() < 1f)
            {
                // add to compass targets
                GameManager.Instance.HudCompass.AddTarget(mission.transform);
            }
        }
    }

    private int GetTotalMissionsCompleted()
    {
        int totalMissionsCompleted = 0;

        foreach (Mission mission in subMissions)
        {
            // if mission is complete
            if (mission.GetProgress() >= 1f)
            {
                // add one to total count
                totalMissionsCompleted++;
            }
        }

        return totalMissionsCompleted;
    }
}