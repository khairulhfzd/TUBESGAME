using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [Header("Respawn Settings")]
    public Transform respawnPoint; // Titik awal respawn

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Darah Player berkurang! Sisa darah: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Fungsi baru untuk memindahkan player
    public void Respawn()
    {
        // 1. Pindahkan posisi player ke titik respawn
        transform.position = respawnPoint.position;

        // 2. Reset kecepatan jatuh (Sangat penting agar player tidak melesat ke bawah setelah respawn)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; 
        }
    }

    // Fungsi baru untuk mengecek sisa darah dari script lain
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        Debug.Log("Player telah mati!");
        // Logika game over kamu nanti masuk ke sini
    }
}