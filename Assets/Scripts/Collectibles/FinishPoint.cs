using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    // Called when the object enters a trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the "Player" tag
        if (collision.CompareTag("Player"))
        {
            // Unlock new level in the game progression
            UnlockNewLevel();

            // Optionally, go to the next level using the SceneController
            SceneController.instance.NextLevel();

            // Check for win condition after loading the next level
            SceneController.instance.CheckWinCondition();
        }
    }

    // Unlock a new level in the game progression
    void UnlockNewLevel()
    {
        // Check if the current level index is greater than or equal to the reached index
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            // Update the reached index to the next level
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);

            // Increment the unlocked level count
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);

            // Save the player preferences
            PlayerPrefs.Save();
        }
    }
}
