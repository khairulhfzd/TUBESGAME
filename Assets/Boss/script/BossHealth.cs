using UnityEngine;
// --- TAMBAHAN: Diperlukan agar fungsi IEnumerator (Coroutine) bisa berjalan ---
using System.Collections;

public class BossHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 1000f;
    public float currentHealth;

    [Header("Phase State")]
    // 0 = Fase Awal (Hanya Basic Attack)
    // 1 = Darah <= 80% (Zenith Feathers terbuka)
    // 2 = Darah <= 60% (Avalanche Crash terbuka)
    // 3 = Darah <= 30% (Divine Execution terbuka)
    public int currentPhase = 0;

    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentPhase = 0;
    }

    // FUNGSI UTAMA: Dipanggil oleh Player untuk mengurangi darah bos
    public void TakeDamage(float damageAmount)
    {
        // Jika bos sudah mati, abaikan semua hit tambahan
        if (isDead) return;

        currentHealth -= damageAmount;
        Debug.Log("Bos Terkena Hit! Sisa Darah: " + currentHealth + " (" + (currentHealth / maxHealth * 100f) + "%)");

        // Cek apakah darah bos menyentuh batas fase baru
        CheckPhase();

        // Cek apakah bos kehabisan darah
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void CheckPhase()
    {
        float healthPercentage = (currentHealth / maxHealth) * 100f;

        // Cek dari persentase terkecil agar tidak terjadi bentrok logika
        if (healthPercentage <= 30f && currentPhase < 3)
        {
            currentPhase = 3;
            Debug.Log("LOG: Bos masuk ke FASE 3! (Divine Execution Unlocked)");
        }
        else if (healthPercentage <= 60f && currentPhase < 2)
        {
            currentPhase = 2;
            Debug.Log("LOG: Bos masuk ke FASE 2! (Avalanche Crash Unlocked)");
        }
        else if (healthPercentage <= 80f && currentPhase < 1)
        {
            currentPhase = 1;
            Debug.Log("LOG: Bos masuk ke FASE 1! (Zenith Feathers Unlocked)");
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("LOG: Bos telah dikalahkan!");

        // Memicu trigger "Die" yang terhubung ke kotak 'dead' di Animator-mu
        anim.SetTrigger("Die");

        // MEMATIKAN SCRIPT PERGERAKAN: Supaya saat mati bos tidak ngejar player lagi
        if (GetComponent<BossMovement>() != null)
        {
            GetComponent<BossMovement>().enabled = false;
        }

        // --- TAMBAHAN: Mematikan Otak Serangan Bos juga saat mati ---
        if (GetComponent<BossSkillManager>() != null)
        {
            GetComponent<BossSkillManager>().enabled = false;
        }

        // Menonaktifkan collider agar jasad bos tidak menghalangi jalan player
        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = false;
        }

        // --- TAMBAHAN: Jalankan fungsi menghilang secara halus (Coroutine) ---
        StartCoroutine(FadeOutAndDestroy());
    }

    // --- FUNGSI BARU: Mengurangi transparansi gambar bos perlahan lalu menghapusnya ---
    IEnumerator FadeOutAndDestroy()
    {
        // 1. Cari SpriteRenderer (bisa di objek utama atau di objek anaknya seperti objek Visual)
        SpriteRenderer bossSprite = GetComponent<SpriteRenderer>();
        if (bossSprite == null)
        {
            bossSprite = GetComponentInChildren<SpriteRenderer>();
        }

        // Jeda waktu tunggu (1.5 detik) memberi kesempatan animasi mati diselesaikan dulu
        yield return new WaitForSeconds(1.5f);

        if (bossSprite != null)
        {
            float fadeSpeed = 1f; // Kecepatan menghilang (makin besar angkanya makin cepat hilang)
            Color startColor = bossSprite.color;

            // Mengurangi nilai Alpha (transparansi) dari solid (1) menuju tembus pandang (0)
            while (startColor.a > 0)
            {
                startColor.a -= fadeSpeed * Time.deltaTime;
                bossSprite.color = startColor;
                yield return null; // Berpindah ke frame selanjutnya
            }
        }

        // 2. Jika sudah benar-benar tidak terlihat, hapus total objek Bos dari game
        Destroy(gameObject);
        Debug.Log("LOG: Objek Bos telah dihancurkan sepenuhnya dari Scene.");
    }
}