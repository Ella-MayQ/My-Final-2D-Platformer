using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    // Start is called before the first frame update
    private void Start()
    {
        // Set the fill amount of the total health bar based on the player's initial health
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    // Update is called once per frame
    private void Update()
    {
        // Update the fill amount of the current health bar based on the player's current health
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
