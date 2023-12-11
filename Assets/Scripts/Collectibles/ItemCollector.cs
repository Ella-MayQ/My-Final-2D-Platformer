using UnityEngine;
using TMPro;
using System.Collections;

public class ItemCollector : MonoBehaviour
{
    private int coins = 0;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject collectParticlesPrefab;

    // Called when the object enters a trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the "Coin" tag
        if (collision.gameObject.CompareTag("Coin"))
        {
            // Instantiate particles at the coin's position
            GameObject particlesObject = Instantiate(collectParticlesPrefab, collision.transform.position, Quaternion.identity);

            // Get the ParticleSystem component from the instantiated prefab
            ParticleSystem collectParticles = particlesObject.GetComponent<ParticleSystem>();

            // Play the particle system
            collectParticles.Play();

            // Stop emitting new particles after a short delay
            StartCoroutine(StopParticlesAfterDelay(collectParticles, 0.5f));

            // Destroy the collided coin object
            Destroy(collision.gameObject);

            // Increment the coin count
            coins++;

            // Update the displayed coin count in the UI text
            coinsText.text = "Coins: " + coins;

            // Play the pickup sound
            SoundManager.instance.PlaySound(pickupSound);

            // Debug log for debugging purposes
            // Debug.Log("Reached point X");

            // Destroy the particle system GameObject after its duration
            Destroy(particlesObject, collectParticles.main.duration);
        }
    }

    // Coroutine to stop emitting particles after a delay
    private IEnumerator StopParticlesAfterDelay(ParticleSystem particles, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Stop emitting particles
        particles.Stop();
    }
}
