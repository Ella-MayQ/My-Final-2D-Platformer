using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // Patrol Points
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    // Enemy
    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    // Movement parameters
    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    // Idle Behaviour
    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    // Enemy Animator
    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Store the initial scale of the enemy
        initScale = enemy.localScale;
    }

    // OnDisable is called when the script is no longer active
    private void OnDisable()
    {
        // Set the "moving" parameter to false when the script is disabled
        anim.SetBool("moving", false);
    }

    // Update is called once per frame
    private void Update()
    {
        // Check the direction and patrol accordingly
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    // Change direction and trigger idle behavior
    private void DirectionChange()
    {
        // Set "moving" to false and increment idle timer
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        // Change direction if idle duration is exceeded
        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    // Move in the specified direction
    private void MoveInDirection(int _direction)
    {
        // Reset idle timer and set "moving" to true
        idleTimer = 0;
        anim.SetBool("moving", true);

        // Make the enemy face the specified direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
           initScale.y, initScale.z);

        // Move the enemy in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
