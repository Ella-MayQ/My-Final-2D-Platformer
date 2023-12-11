using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //private int currentLevelIndex = 0;

    //public string YouWinTag = "YouWin";

    //void Awake()
    //{
        // Ensure that this GameObject persists across scenes
        //DontDestroyOnLoad(this.gameObject);
    //}

    //void Update()
    //{
        // Check for level completion criteria
    //    if (IsLevelComplete())
    //    {
            // Load the next level or show "You Win" if it's the last level
    //        LoadNextLevel();
    //    }
    //}

    //bool IsLevelComplete()
    //{
        // Check if the finish line is triggered
    //    return GameObject.FindGameObjectWithTag(YouWinTag) == null;
    //}

    //public void LoadNextLevel()
    //{
    //    currentLevelIndex++;

     //   if (currentLevelIndex < SceneManager.sceneCountInBuildSettings)
     //   {
     //       SceneManager.LoadScene(currentLevelIndex);
     //   }
     //   else
     //   {
            // All levels are completed, show "You Win" scene
     //       SceneManager.LoadScene("YouWin");
      //  }
    //}
}
