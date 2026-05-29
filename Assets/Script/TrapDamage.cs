/*using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damageAmount = 20;

    // Perhatikan perubahannya: sekarang menggunakan OnCollisionEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mendeteksi tabrakan fisik dengan Player
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
*/