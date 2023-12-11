using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Attack Parameters
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    // Collider Parameters
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    // Player Layer for detection
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    // Cooldown timer for attacks
    private float cooldownTimer = Mathf.Infinity;

    // Attack Sound
    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    // References
    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get components for reference
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Increment cooldown timer
        cooldownTimer += Time.deltaTime;

        // Attack only when player in sight and cooldown is ready
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
                DamagerPlayer();
                SoundManager.instance.PlaySound(attackSound);
            }
        }

        // Disable enemy patrol if player in sight
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    // Check if the player is in sight using raycasting
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        // If player is in sight, get the player's health component
        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    // Draw visual representation of the detection range in the Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // Damage the player if still in range
    private void DamagerPlayer()
    {
        // If player still in range, damage them
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
}

