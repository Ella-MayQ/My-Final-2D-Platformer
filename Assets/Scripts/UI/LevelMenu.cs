using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    int levelsUnlocked;
    public Button[] buttons;

    private void Start()
    {
        // Retrieve the number of levels unlocked from player preferences
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        // Update the interactability of buttons based on the number of levels unlocked
        UpdateButtonInteractability();
    }

    // UpdateButtonInteractability updates the interactability of buttons based on the number of levels unlocked
    void UpdateButtonInteractability()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            // Buttons are interactable if their index is less than the number of levels unlocked
            buttons[i].interactable = i < levelsUnlocked;
        }
    }

    // SaveProgress saves the current progress (number of levels unlocked) to player preferences
    void SaveProgress()
    {
        PlayerPrefs.SetInt("levelsUnlocked", levelsUnlocked);
        PlayerPrefs.Save();
    }

    // LoadLevel is called when a level button is clicked
    public void LoadLevel(int levelIndex)
    {
        // Check if the selected level is unlocked
        if (levelIndex <= levelsUnlocked)
        {
            // Load the selected level
            SceneManager.LoadScene(levelIndex);

            // Assuming that completing a level unlocks the next level
            if (levelIndex == levelsUnlocked && levelIndex < buttons.Length)
            {
                // Increment the number of levels unlocked
                levelsUnlocked++;

                // Update the button interactability
                UpdateButtonInteractability();

                // Save the progress
                SaveProgress();
            }
        }
    }
}
