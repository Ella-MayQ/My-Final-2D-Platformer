using UnityEngine;

public class Spikehead : Enemy_Damage
{
    // SpikeHead attributes
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    // Array to store four directions (right, left, up, down)
    private Vector3[] directions = new Vector3[4];

    // Destination where the Spikehead is moving
    private Vector3 destination;

    // Timer for checking for the player
    private float checkTimer;

    // Flag to indicate if the Spikehead is attacking
    private bool attacking;

    // Sound effect played on impact
    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    // Called when the object becomes enabled and active
    private void OnEnable()
    {
        // Stop Spikehead movement
        Stop();
    }

    // Update is called once per frame
    private void Update()
    {
        // Move Spikehead to destination only if attacking
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            // Increment check timer
            checkTimer += Time.deltaTime;

            // Check for player after the specified delay
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    // Check for the player in different directions
    private void CheckForPlayer()
    {
        // Calculate four directions based on the current position
        CalculateDirections();

        // Loop through each direction
        for (int i = 0; i < directions.Length; i++)
        {
            // Debug draw a ray in each direction
            Debug.DrawRay(transform.position, directions[i], Color.red);

            // Raycast to check for the player in each direction within the specified range
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            // If the ray hits a collider and the Spikehead is not already attacking, start attacking
            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    // Calculate four directions based on the right, left, up, and down vectors
    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    // Stop Spikehead movement
    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    // OnTriggerEnter2D is called when another collider enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Play impact sound, call the base method, and stop Spikehead movement
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
