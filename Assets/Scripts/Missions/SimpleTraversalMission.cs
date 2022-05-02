using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A mission handling the player's arrival at the resource cache.
/// </summary>
public class SimpleTraversalMission : Mission
{
    private bool hasPlayerEntered = false;

    /// <inheritdoc/>
    public override float GetProgress()
    {
        return hasPlayerEntered ? 1f : 0f;
    }

    /// <summary>
    /// Notifies the MissionManager that the player has entered the trigger and the next step should begin.
    /// </summary>
    public void OnPlayerEnteredLocation()
    {
        hasPlayerEntered = true;
        NotifyOfProgress();
    }
}