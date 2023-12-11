using UnityEngine;

public class Door : MonoBehaviour
{
    // References to the previous and next rooms
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;

    // Reference to the CameraController
    [SerializeField] private CameraController cam;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the CameraController component from the main camera
        cam = Camera.main.GetComponent<CameraController>();
    }

    // OnTriggerEnter2D is called when another collider enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the "Player" tag
        if (collision.tag == "Player")
        {
            // Check the player's position relative to the door's position
            if (collision.transform.position.x < transform.position.x)
            {
                // Move the camera to the next room
                cam.MoveToNewRoom(nextRoom);

                // Activate the next room and deactivate the previous room
                nextRoom.GetComponent<Room>().ActivateRoom(true);
                previousRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else
            {
                // Move the camera to the previous room
                cam.MoveToNewRoom(previousRoom);

                // Activate the previous room and deactivate the next room
                previousRoom.GetComponent<Room>().ActivateRoom(true);
                nextRoom.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }
}
