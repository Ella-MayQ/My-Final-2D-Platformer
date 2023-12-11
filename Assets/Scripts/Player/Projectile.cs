using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get components for reference
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // If the projectile has hit, stop its movement
        if (hit) return;

        // Move the projectile in the specified direction
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        // Track the lifetime of the projectile and deactivate it after a certain time
        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    // Called when the projectile collides with another Collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mark the projectile as hit and trigger the explode animation
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        // If the collision is with an enemy, damage the enemy
        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }

    // Method to set the direction and reset the projectile
    public void SetDirection(float _direction)
    {
        // Reset lifetime, direction, and enable the projectile
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // Flip the projectile sprite based on the direction
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    // Method to deactivate the projectile
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
