using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int damageAmount = 5;

    // Fungsi ini otomatis berjalan karena kita menggunakan "Is Trigger" pada Collider duri
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Periksa apakah objek yang menyentuh duri memiliki tag "Player"
        if (other.CompareTag("Player"))
        {
            // Ambil komponen PlayerHealth dari objek yang menabrak
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}