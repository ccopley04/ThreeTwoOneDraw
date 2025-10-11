using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false; // Tracks whether the game is paused

    // Method to toggle pause
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game by setting time scale to 0
            // Debug.Log("Game Paused");

            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = 1f; // Resume the game by setting time scale to 1
            // Debug.Log("Game Resumed");

            AudioListener.pause = false;
        }
    }

    public bool getIsPaused()
    {
        return isPaused;
    }


}
