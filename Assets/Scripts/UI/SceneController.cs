using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Singleton instance
    public static SceneController instance;

    private void Awake()
    {
        // Implementing a simple Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // If an instance already exists, destroy the duplicate
            Destroy(gameObject);
        }
    }

    // Move to the next level
    public void NextLevel()
    {
        // Get the index of the next scene in build settings
        int nextBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if there's another scene to load
        if (nextBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene
            SceneManager.LoadScene(nextBuildIndex);
        }
        else
        {
            // Handle the case when there are no more levels
            Debug.LogWarning("No more levels available.");
        }
    }

    // Check if the player has won the game
    public void CheckWinCondition()
    {
        // Check if the current scene is the last one
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            // Show the YouWinScreen after completing the last level
            UIManager.instance.YouWin();
        }
    }
}
