using UnityEngine;

public class BossFollowPlayer : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform playerTransform; // Tarik objek Player kamu ke slot ini di Inspector

    [Header("Movement Settings")]
    public float moveSpeed = 3f;      // Kecepatan gerak Boss
    public float stoppingDistance = 1.5f; // Jarak aman di mana Boss akan berhenti mengejar (jarak attack)

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Mengambil komponen SpriteRenderer untuk membalik arah sprite (flip)
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Mengantisipasi jika lupa memasukkan target Player di Inspector
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Hitung jarak antara Boss dan Player
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Jika jaraknya lebih besar dari jarak berhenti, maka Boss akan mengejar
        if (distanceToPlayer > stoppingDistance)
        {
            // Hitung arah menuju Player
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Gerakkan Boss menuju posisi Player
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }

        // Jalankan fungsi untuk mengatur arah hadap wajah Boss
        FlipTowardsPlayer();
    }

    void FlipTowardsPlayer()
    {
        // Jika posisi Player berada di sebelah kiri Boss, balik sprite ke kiri (atau sebaliknya tergantung orientasi awal sprite)
        if (playerTransform.position.x < transform.position.x)
        {
            // Sesuaikan true/false ini dengan arah default sprite sheet kamu
            spriteRenderer.flipX = true;
        }
        else if (playerTransform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }
}