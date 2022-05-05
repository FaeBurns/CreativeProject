using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

/// <summary>
/// A mission responsible for displaying a failure screen.
/// </summary>
public class MissionBriefing : Mission
{
    [SerializeField] private DisplayableMessage message;

    private bool finishedBrief = false;

    /// <inheritdoc/>
    public override string MissionStatement => string.Empty;

    /// <inheritdoc/>
    public override float GetProgress()
    {
        return finishedBrief ? 1f : 0f;
    }

    /// <inheritdoc/>
    protected override void Begin()
    {
        // get MessageDisplay
        MessageDisplay messageDisplay = GameManager.Instance.MessageDisplay;

        // Display Message
        messageDisplay.DisplayMessage(message, OnMessageFinished);
    }

    /// <inheritdoc/>
    protected override void Cancel()
    {
        GameManager.Instance.MessageDisplay.Stop();
    }

    private void OnMessageFinished()
    {
        finishedBrief = true;
        NotifyOfProgress();
    }
}