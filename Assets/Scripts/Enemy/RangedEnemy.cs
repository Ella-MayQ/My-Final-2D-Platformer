using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    // Attack parameters
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    // Ranged Attack parameters
    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    // Collider parameters
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    // Player Layer for detection
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    // Timer for attack cooldown
    private float cooldownTimer = Mathf.Infinity;

    // Sound for ranged attack
    [Header("Fireball Sound")]
    [SerializeField] private AudioClip fireballSound;

    // References
    private Animator anim;
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

        // Attack only when player in sight
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("rangedAttack");
            }
        }

        // Disable enemy patrol if player in sight
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    // Method for performing ranged attack
    private void RangedAttack()
    {
        // Play sound for the ranged attack
        SoundManager.instance.PlaySound(fireballSound);

        // Reset cooldown timer
        cooldownTimer = 0;

        // Activate and position the next available fireball
        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    // Find the index of the next available fireball
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    // Check if the player is in sight using raycasting
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    // Draw visual representation of the detection range in the Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
