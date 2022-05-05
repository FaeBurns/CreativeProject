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
    [SerializeField] private string[] message;
    [SerializeField] private float timeToWaitFor = 3f;
    [SerializeField] private float fadeTime = 0.5f;

    [SerializeField] private CanvasGroup fadeGroup;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private bool startFaded = false;
    [SerializeField] private bool endFaded = false;

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
        StartCoroutine(DisplayAndFadeOut(message));
    }

    private IEnumerator DisplayAndFadeOut(string[] wholeMessage)
    {
        if (startFaded)
        {
            fadeGroup.alpha = 1;
        }
        else
        {
            // fade background in
            yield return FadeGroup(fadeTime, 0, 1);
        }

        // display message in order
        for (int i = 0; i < wholeMessage.Length; i++)
        {
            // display part of message
            yield return DisplayMessage(wholeMessage[i], timeToWaitFor);
        }

        if (!endFaded)
        {
            // fade background out
            yield return FadeGroup(fadeTime, 1, 0);
        }

        finishedBrief = true;
        NotifyOfProgress();
    }

    private IEnumerator DisplayMessage(string message, float waitTime)
    {
        // set text
        messageText.text = message.Replace("\\n", "\n");

        // fade text in
        yield return FadeText(fadeTime, 0, 1);

        // wait
        yield return new WaitForSeconds(waitTime);

        // fade text out
        yield return FadeText(fadeTime, 1, 0);
    }

    private IEnumerator FadeText(float timeToFade, float start, float end)
    {
        // fade
        float startTime = Time.time;
        float endTime = Time.time + timeToFade;

        // while time has not yet passed
        while (Time.time <= endTime)
        {
            // get alpha
            float alpha = Mathf.InverseLerp(startTime, endTime, Time.time);

            // set visual alpha
            messageText.alpha = Mathf.Lerp(start, end, alpha);

            // wait for next frame
            yield return null;
        }

        messageText.alpha = end;
    }

    private IEnumerator FadeGroup(float timeToFade, float start, float end)
    {
        // fade
        float startTime = Time.time;
        float endTime = Time.time + timeToFade;

        // while time has not yet passed
        while (Time.time <= endTime)
        {
            // get alpha
            float alpha = Mathf.InverseLerp(startTime, endTime, Time.time);

            // set visual alpha
            fadeGroup.alpha = Mathf.Lerp(start, end, alpha);

            // wait for next frame
            yield return null;
        }

        fadeGroup.alpha = end;
    }
}