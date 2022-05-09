using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A component responsible for managing the game state.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// A global reference to the <see cref="MissionManager"/>.
    /// </summary>
    public MissionManager MissionManager;

    /// <summary>
    /// A global reference to the <see cref="MessageDisplay"/>.
    /// </summary>
    public MessageDisplay MessageDisplay;

    /// <summary>
    /// A global reference to the <see cref="ResourceTracker"/>.
    /// </summary>
    public ResourceTracker ResourceTracker;

    /// <summary>
    /// A global reference to the <see cref="HudCompass"/>.
    /// </summary>
    public HudCompass HudCompass;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameManager"/> class.
    /// </summary>
    public GameManager()
    {
        if (Instance != null)
        {
            Debug.LogError("GameManager instance already exists");
        }

        Instance = this;
    }

    /// <summary>
    /// Gets a reference to the static GameManager instance.
    /// </summary>
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Restarts the game.
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Instance = null;
    }

    private void Update()
    {
        // disable lock and then reset it
        // if it is not done this way, the lock does not stay active
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

#if !UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }
}
