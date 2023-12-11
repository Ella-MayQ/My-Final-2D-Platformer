using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject levelsPanel;
    [SerializeField] private GameObject mainMenuButtons;

    void Start()
    {
        // Assuming levelsPanel and mainMenuButtons are initially inactive, you may need to set them active based on your specific setup.
        levelsPanel.SetActive(false);
        mainMenuButtons.SetActive(true);
    }

    public void PlayGame()
    {
        // Load the game scene or perform other actions to start the game
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit(); // Quits the game (only works in build)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Exits play mode
#endif
    }

    void Update()
    {
        // Check for the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Call a method to handle the exit from the levels panel
            ExitLevelsPanel();
        }
    }

    // Method to handle exiting the levels panel
    private void ExitLevelsPanel()
    {
        // Logic to close the levels panel
        levelsPanel.SetActive(false);

        // Activate the main menu buttons
        mainMenuButtons.SetActive(true);
    }
}
