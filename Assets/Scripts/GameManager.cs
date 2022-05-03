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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
