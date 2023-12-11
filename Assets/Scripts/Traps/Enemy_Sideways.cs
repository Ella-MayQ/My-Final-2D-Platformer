using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        // Calculate the left and right edges based on the initial position and movement distance
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        // Move the enemy left or right within the specified distance
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                // Move left
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                // Change direction when reaching the left edge
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                // Move right
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                // Change direction when reaching the right edge
                movingLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the "Player" tag
        if (collision.tag == "Player")
        {
            // Get the Health component from the player and apply damage
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
