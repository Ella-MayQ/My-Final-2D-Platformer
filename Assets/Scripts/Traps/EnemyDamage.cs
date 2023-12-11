using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    [SerializeField] protected float damage;

    // Called when another collider enters the trigger zone
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the "Player" tag
        if (collision.tag == "Player")
        {
            // Get the Health component from the player and apply damage
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
