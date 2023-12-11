using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("You Win")]
    [SerializeField] private GameObject youWinScreen;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one UIManager instance exists
        }

        // Set initial UI states
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        youWinScreen.SetActive(false);
    }

    #region Game Over Functions
    // Game over function
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    // Restart level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Activate main menu
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); // Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Exits play mode
#endif
    }
    #endregion

    private void Update()
    {
        // Toggle pause screen with the space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    #region You Win Functions
    // You win function
    public void YouWin()
    {
        youWinScreen.SetActive(true);
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        // If status is true, pause; if status is false, unpause
        pauseScreen.SetActive(status);

        // When pause status is true, change timescale to 0 (time stops);
        // when it's false, change it back to 1 (time goes by normally)
        Time.timeScale = status ? 0 : 1;
    }
    #endregion

    // Called when the player collides with the "YouWin" tag
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("YouWin"))
        {
            YouWin();
        }
    }
}
