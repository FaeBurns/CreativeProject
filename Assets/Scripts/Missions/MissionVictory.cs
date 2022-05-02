using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A mission handling a game win condition.
/// </summary>
public class MissionVictory : Mission
{
    /// <inheritdoc/>
    public override float GetProgress()
    {
        return 0;
    }

    /// <inheritdoc/>
    protected override void Begin()
    {
        Debug.Log("Game won!");
    }
}