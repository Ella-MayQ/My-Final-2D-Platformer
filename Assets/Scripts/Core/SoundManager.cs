using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singleton pattern to ensure only one instance of SoundManager exists
    public static SoundManager instance { get; private set; }

    // Audio sources for sound effects and background music
    private AudioSource soundSource;
    private AudioSource musicSource;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get AudioSource components from the current GameObject
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        // Keep this object even when transitioning to a new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Destroy duplicate game objects to maintain singleton pattern
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }

    // Method to play a sound effect
    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }
}
