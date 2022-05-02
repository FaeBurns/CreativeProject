using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A mission containing a default statement.
/// </summary>
public abstract class StatementedMission : Mission
{
    [SerializeField] private string missionStatement;

    /// <inheritdoc/>
    public override string MissionStatement => missionStatement;
}