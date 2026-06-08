using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Pergerakan")]
    public float moveSpeed = 3f;      // Kecepatan terbang si bos
    public float attackRange = 2.5f;  // Jarak minimal sebelum bos mulai mengayunkan kapak

    [Header("Referensi")]
    private Transform player;         // Tempat menyimpan koordinat Player
    private Animator anim;
    private bool isFacingRight = false; // Atur ke TRUE jika di sprite asal bosmu menghadap KANAN

    void Start()
    {
        anim = GetComponent<Animator>();

        // Mencari objek dengan Tag "Player" secara otomatis di dalam game
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Waduh! Objek dengan Tag 'Player' tidak ditemukan di Scene. Pastikan player buatan temanmu sudah diberi Tag 'Player'!");
        }
    }

    void Update()
    {
        // Jika player tidak ditemukan atau mati, bos diam saja
        if (player == null) return;

        // 1. Hitung jarak antara Bos dan Player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (GetComponent<BossSkillManager>() != null && GetComponent<BossSkillManager>().isBusy)
        {
            anim.SetFloat("Speed", 0f); // Kembali ke animasi idle
            return;
        }

        // 2. Logika Pergerakan & Sinkronisasi ke Animator
        if (distanceToPlayer > attackRange)
        {
            // Jika player masih jauh, kejar!
            MoveTowardsPlayer();
            anim.SetFloat("Speed", 1f); // Mengubah parameter Speed di Animator > 0.1 (Masuk state Chasing)
        }
        else
        {
            // Jika sudah dekat, berhenti (Nanti di sini tempat memicu Phantom Cleave)
            anim.SetFloat("Speed", 0f); // Mengubah parameter Speed di Animator < 0.1 (Kembali ke Idle)
        }

        // 3. Logika Membalikkan Badan
        LookAtPlayer();
    }

    void MoveTowardsPlayer()
    {
        // Karena Aethelgard adalah bos terbang (Hovering), dia akan mengejar koordinat X dan Y player
        Vector2 targetPosition = new Vector2(player.position.x, player.position.y);

        // Menggerakkan posisi bos perlahan menuju posisi player
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void LookAtPlayer()
    {
        // Jika posisi player ada di sebelah kanan bos, tapi bos menghadap kiri -> FLIP!
        if (player.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        // Jika posisi player ada di sebelah kiri bos, tapi bos menghadap kanan -> FLIP!
        else if (player.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        // Membalik arah gambar dengan mengalikan skala X dengan -1
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}