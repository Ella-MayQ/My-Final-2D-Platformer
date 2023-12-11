using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickupSound;

    // Called when another Collider2D enters the trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider has the "Player" tag
        if (collision.CompareTag("Player"))
        {
            // Play the pickup sound
            SoundManager.instance.PlaySound(pickupSound);

            // Retrieve the Health component from the player and add health
            collision.GetComponent<Health>().AddHealth(healthValue);

            // Deactivate the collectible object
            gameObject.SetActive(false);
        }
    }
}
