using UnityEngine;

public class EnemyFireballHolder : MonoBehaviour
{
    // Reference to the enemy's Transform
    [SerializeField] private Transform enemy;

    // Update is called once per frame
    private void Update()
    {
        // Set the local scale of this object to match the enemy's local scale
        transform.localScale = enemy.localScale;
    }
}
