using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPosition;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Save the initial positions of the enemies
        initialPosition = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            // Check if the enemy is not null before saving its initial position
            if (enemies[i] != null)
                initialPosition[i] = enemies[i].transform.position;
        }
    }

    // Method to activate or deactivate the room and reset enemy positions
    public void ActivateRoom(bool _status)
    {
        // Loop through each enemy in the room
        for (int i = 0; i < enemies.Length; i++)
        {
            // Check if the enemy is not null
            if (enemies[i] != null)
            {
                // Set the enemy's active status and reset its position
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }
        }
    }
}
