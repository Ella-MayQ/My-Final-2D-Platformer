using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    // Health parameters
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    // Invulnerability parameters
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    // Components to disable when the player is dead
    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    // Death Sound
    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Initialize health and get components for reference
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Method to handle taking damage
    public void TakeDamage(float _damage)
    {
        // Ignore damage if currently invulnerable
        if (invulnerable) return;

        // Clamp health within the specified range
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        // If the player is still alive, play hurt animation and trigger invulnerability
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        // If the player is dead, disable components and play death animation
        else
        {
            if (!dead)
            {
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }

    // Method to add health
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    // Method to respawn the player
    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");

        // Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }

    // Coroutine for handling invulnerability frames
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    // Method to deactivate the game object
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
