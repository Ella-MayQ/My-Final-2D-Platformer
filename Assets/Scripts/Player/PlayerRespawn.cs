using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get components for reference
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Method to handle player respawn
    public void CheckRespawn()
    {
        // Check if a checkpoint is available
        if (currentCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }

        // Respawn the player, restore health, and reset animation
        playerHealth.Respawn();
        transform.position = currentCheckpoint.position; // Move player to checkpoint location

        // Move the camera to the checkpoint's room
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    // Called when the player enters a collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider is a checkpoint
        if (collision.gameObject.tag == "Checkpoint")
        {
            // Set the current checkpoint to the one the player entered
            currentCheckpoint = collision.transform;

            // Play the checkpoint sound
            SoundManager.instance.PlaySound(checkpoint);

            // Disable the collider to prevent repeated triggers
            collision.GetComponent<Collider2D>().enabled = false;

            // Trigger the appearance animation of the checkpoint
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
