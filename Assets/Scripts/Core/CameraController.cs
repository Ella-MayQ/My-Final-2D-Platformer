using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Room camera variables
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    // Follow player variables
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    // Called every frame
    private void Update()
    {
        // Follow player
        // Update the camera position to follow the player with a look-ahead distance
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);

        // Smoothly interpolate the look-ahead distance based on player scale
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    // Move the camera to a new room
    public void MoveToNewRoom(Transform _newRoom)
    {
        // Set the target X position for the room camera
        currentPosX = _newRoom.position.x;
    }
}
