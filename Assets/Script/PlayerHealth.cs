using UnityEngine;
using UnityEngine.UI; // WAJIB ditambahkan agar bisa mengendalikan komponen UI Slider

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [Header("UI Settings")]
    public Slider healthSlider; // Tempat memasukkan UI Slider di Inspector

    [Header("Respawn Settings")]
    public Transform respawnPoint;

    private void Start()
    {
        currentHealth = maxHealth;

        // Mengatur nilai maksimal dan nilai saat ini pada UI Slider secara otomatis di awal game
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Darah Player berkurang! Sisa darah: " + currentHealth);

        // Memperbarui visual bilah nyawa pada UI Slider
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Respawn()
    {
        transform.position = respawnPoint.position;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; 
        }

        // Opsional: Jika ingin darah penuh kembali saat respawn setelah jatuh
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.value = maxHealth;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        Debug.Log("Player telah mati!");
    }
}