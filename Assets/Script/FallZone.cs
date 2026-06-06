using UnityEngine;

public class FallZone : MonoBehaviour
{
    [SerializeField] private int fallDamage = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Mengecek apakah yang jatuh adalah player
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // 1. Kurangi darah player
                playerHealth.TakeDamage(fallDamage);

                // 2. Jika masih hidup setelah jatuh, kembalikan ke titik respawn
                if (playerHealth.GetCurrentHealth() > 0)
                {
                    playerHealth.Respawn();
                }
            }
        }
    }
}