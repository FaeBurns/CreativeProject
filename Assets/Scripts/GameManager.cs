using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component responsible for managing the game state.
/// </summary>
public class GameManager : MonoBehaviour
{
    private void Update()
    {
        // disable lock and then reset it
        // if it is not done this way, the lock does not stay active
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
