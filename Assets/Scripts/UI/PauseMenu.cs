using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject pauseMenu;

    // Called when the "Home" button is pressed
    public void Home()
    {
        Debug.Log("Home button pressed");
        SceneManager.LoadScene(0);
        // Ensure the game is unpaused when going to the main menu
        Time.timeScale = 1;
    }

    // Called when the "Resume" button is pressed
    public void Resume()
    {
        // Deactivate the pause menu and resume the game
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    // Called when the "Restart" button is pressed
    public void Restart()
    {
        // Restart the current scene and ensure the game is unpaused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    // Toggles the visibility of the pause menu and pauses/unpauses the game
    void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        // If the pause menu is active, set the time scale to 0 to pause the game.
        // If it's inactive, set the time scale to 1 to resume the game.
        Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
    }

    void Update()
    {
        // Check for user input to trigger the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }
}
