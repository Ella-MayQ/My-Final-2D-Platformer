using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    public GameObject bullet;
    public Transform player;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get components for reference
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for attack input and cooldown
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        // Increment cooldown timer
        cooldownTimer += Time.deltaTime;
    }

    // Method to handle the player's attack
    private void Attack()
    {
        // Play the fireball sound
        SoundManager.instance.PlaySound(fireballSound);

        // Reset cooldown timer
        cooldownTimer = 0;

        // Instantiate a bullet at the firePoint position with the player's rotation
        Instantiate(bullet, firePoint.transform.position, player.rotation);
    }

    // Method to find an inactive fireball in the array
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
