using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    // Damage inflicted by the firetrap
    [SerializeField] private float damage;

    // Firetrap activation and active timers
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    // References to components
    private Animator anim;
    private SpriteRenderer spriteRend;

    // Sound effect played when the firetrap is activated
    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    // Flags to track the state of the firetrap
    private bool triggered; // When the trap gets triggered
    private bool active;    // When the trap is active and can hurt the player

    // Reference to the player's health
    private Health playerHealth;

    // Called when the object becomes enabled and active
    private void Awake()
    {
        // Get references to components
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // If the player is in the trigger zone and the trap is active, damage the player
        if (playerHealth != null && active)
            playerHealth.TakeDamage(damage);
    }

    // OnTriggerEnter2D is called when another collider enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.tag == "Player")
        {
            // Get the player's health component
            playerHealth = collision.GetComponent<Health>();

            // If the trap hasn't been triggered, start the activation coroutine
            if (!triggered)
                StartCoroutine(ActivateFiretrap());

            // If the trap is active, damage the player
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    // OnTriggerExit2D is called when another collider exits the trigger zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the colliding object is the player, set playerHealth to null
        if (collision.tag == "Player")
            playerHealth = null;
    }

    // Coroutine to activate and deactivate the firetrap
    private IEnumerator ActivateFiretrap()
    {
        // Turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red;

        // Wait for the activation delay, play the firetrap sound, turn on animation, and return the color back to normal
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white; // Turn the sprite back to its initial color
        active = true;
        anim.SetBool("activated", true);

        // Wait for the active time, deactivate the trap, and reset all variables and the animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
