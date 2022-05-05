using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A <see cref="ScriptableObject"/> responsible for containing data about a full screen message.
/// </summary>
[CreateAssetMenu(fileName = "Message", menuName = "ScriptableObjects/Message")]
public class DisplayableMessage : ScriptableObject
{
    /// <summary>
    /// The sections of the message.
    /// </summary>
    public string[] Sections;

    /// <summary>
    /// The time to display each section for before moving on to the next.
    /// </summary>
    public float TimeToWaitFor = 3f;

    /// <summary>
    /// The time it should take to fade a message in or out.
    /// </summary>
    public float FadeTime = 0.5f;

    /// <summary>
    /// Should the message start with the screen already black.
    /// </summary>
    public bool StartFaded = false;

    /// <summary>
    /// Should the messsage end with the screen still black.
    /// </summary>
    public bool EndFaded = false;
}