using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // You can change the key to pause the game
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1; // Resume the game
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0; // Pause the game
            isPaused = true;
        }
    }
}
