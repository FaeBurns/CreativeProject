using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

/// <summary>
/// A component responsible for handling the display of full screen messages.
/// </summary>
public class MessageDisplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeGroup;
    [SerializeField] private TextMeshProUGUI messageText;

    private Coroutine currentMessage = null;
    private Action completionCallback = null;

    /// <summary>
    /// Gets a value indicating whether there is a message in the process of being displayed.
    /// </summary>
    public bool CurrentlyDisplaying { get; private set; }

    /// <summary>
    /// Displays a message to the screen.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="onCompletionCallback">The callback function called when the message has finished displaying.</param>
    public void DisplayMessage(DisplayableMessage message, Action onCompletionCallback)
    {
        if (CurrentlyDisplaying)
        {
            Debug.LogError("Tried to display a new message while one is already being displayed");
            Stop(false);
        }

        completionCallback = onCompletionCallback;
        currentMessage = StartCoroutine(DisplayAndFadeOut(message, onCompletionCallback));
    }

    /// <summary>
    /// Stops the currently playing message from continuing and instantly hides the UI.
    /// </summary>
    /// <param name="fadeOut">Should the UI fade out or just dissapear.</param>
    /// <param name="runCompletionCallback">Should the onCompletionCallback still be executed.</param>
    public void Stop(bool fadeOut = true, bool runCompletionCallback = false)
    {
        // exit if not currently displaying anything
        if (!CurrentlyDisplaying)
        {
            return;
        }

        StopCoroutine(currentMessage);

        if (runCompletionCallback)
        {
            completionCallback?.Invoke();
        }

        if (fadeOut)
        {
            // do not do straight fade out - might already be in the process of fading out.
            StartCoroutine(Fade(0.5f, 0, 1, a =>
            {
                fadeGroup.alpha -= a;
                messageText.alpha -= a;
            }));
        }
        else
        {
            // we have not been instructed to fade out - just immediately set their alpha to 0
            fadeGroup.alpha = 0;
            messageText.alpha = 0;
        }

        CurrentlyDisplaying = false;
    }

    private IEnumerator DisplayAndFadeOut(DisplayableMessage message, Action onCompletionCallback)
    {
        CurrentlyDisplaying = true;

        // check if we should start already faded in
        if (message.StartFaded)
        {
            fadeGroup.alpha = 1;
        }
        else
        {
            // fade background in
            yield return FadeGroup(message.FadeTime, 0, 1);
        }

        // display message in order
        for (int i = 0; i < message.Sections.Length; i++)
        {
            // display part of message
            yield return DisplayMessage(message.Sections[i], message.TimeToWaitFor, message.FadeTime);
        }

        // notify caller of completion.
        // do this before we've finished fading out so that the player does not notice anything weird.
        onCompletionCallback();

        if (!message.EndFaded)
        {
            // fade background out
            yield return FadeGroup(message.FadeTime, 1, 0);
        }

        // clear
        CurrentlyDisplaying = false;
    }

    private IEnumerator DisplayMessage(string message, float waitTime, float fadeTime)
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
        yield return Fade(timeToFade, start, end, a => messageText.alpha = a);
    }

    private IEnumerator FadeGroup(float timeToFade, float start, float end)
    {
        yield return Fade(timeToFade, start, end, a => fadeGroup.alpha = a);
    }

    private IEnumerator Fade(float timeToFade, float start, float end, Action<float> fadeAction)
    {
        // fade
        float startTime = Time.time;
        float endTime = Time.time + timeToFade;

        // while time has not yet passed
        while (Time.time <= endTime)
        {
            // get alpha
            float lerpAlpha = Mathf.InverseLerp(startTime, endTime, Time.time);

            // get visual alpha
            float alpha = Mathf.Lerp(start, end, lerpAlpha);

            // set value
            fadeAction?.Invoke(alpha);

            // wait for next frame
            yield return null;
        }

        fadeAction?.Invoke(end);
    }
}