using UnityEngine;

public class EnemyProjectile : Enemy_Damage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;
    private bool hit;

    // Called when the object becomes enabled and active
    private void Awake()
    {
        // Get references to components
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Activates the projectile
    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        // If the projectile has hit something, do nothing
        if (hit) return;

        // Move the projectile
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        // Increment the lifetime of the projectile
        lifetime += Time.deltaTime;

        // If the lifetime exceeds the reset time, deactivate the projectile
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    // OnTriggerEnter2D is called when another collider enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set the hit flag to true and call the base class method
        hit = true;
        base.OnTriggerEnter2D(collision);

        // Disable the collider
        coll.enabled = false;

        // Trigger the explode animation or deactivate the projectile if no animation is present
        if (anim != null)
            anim.SetTrigger("explode");
        else
            gameObject.SetActive(false);
    }

    // Deactivates the projectile
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
