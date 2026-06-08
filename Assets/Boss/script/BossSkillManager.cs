using UnityEngine;

public class BossSkillManager : MonoBehaviour
{
    private BossHealth bossHealth;
    private Animator anim;
    private Transform player;

    [Header("Jeda Serangan (Cooldown)")]
    public float basicAttackCooldown = 2f;   // Jeda tebasan kapak (Phantom Cleave)
    public float specialSkillCooldown = 7f;  // Jeda jurus spesial antar fase

    private float basicAttackTimer = 0f;
    private float specialSkillTimer = 0f;
    private float attackRange;

    [Header("Status Kontrol")]
    public bool isBusy = false; // Saklar penanda: Apakah bos sedang mengayunkan senjata/jurus?

    void Start()
    {
        bossHealth = GetComponent<BossHealth>();
        anim = GetComponent<Animator>();

        // Mengambil jangkauan serang otomatis dari script BossMovement
        if (GetComponent<BossMovement>() != null)
        {
            attackRange = GetComponent<BossMovement>().attackRange;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Jika bos sedang sibuk menyerang, kunci semua timer dan gerakan bawah
        if (isBusy) return;

        // Jalankan timer di belakang layar setiap detik
        basicAttackTimer += Time.deltaTime;

        // Skill spesial hanya akan mulai dicharge jika bos sudah terluka (Masuk fase 1 ke atas)
        if (bossHealth.currentPhase > 0)
        {
            specialSkillTimer += Time.deltaTime;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // PRIORITAS 1: Cek apakah Timer Skill Spesial sudah penuh?
        if (specialSkillTimer >= specialSkillCooldown)
        {
            TriggerSpecialSkill();
            return; // Keluar dari Update agar tidak mengeksekusi basic attack bersamaan
        }

        // PRIORITAS 2: Cek apakah player sudah dekat DAN timer kapak sudah siap?
        if (distanceToPlayer <= attackRange && basicAttackTimer >= basicAttackCooldown)
        {
            TriggerBasicAttack();
        }
    }

    void TriggerBasicAttack()
    {
        isBusy = true;
        basicAttackTimer = 0f;

        anim.SetTrigger("Attack"); // Memicu serangan Phantom Cleave
        Debug.Log("Bos Menggunakan: Phantom Cleave (Basic Attack)");
    }

    void TriggerSpecialSkill()
    {
        isBusy = true;
        specialSkillTimer = 0f;

        // Pilih jurus mematikan berdasarkan fase dari script BossHealth
        switch (bossHealth.currentPhase)
        {
            case 1:
                anim.SetTrigger("CastZenith");
                Debug.Log("Bos Menggunakan Skill Fase 1: Zenith Feathers!");
                break;
            case 2:
                anim.SetTrigger("CastAvalanche");
                Debug.Log("Bos Menggunakan Skill Fase 2: Avalanche Crash!");
                break;
            case 3:
                anim.SetTrigger("CastDivine");
                Debug.Log("Bos Menggunakan Skill Fase 3: Divine Execution!");
                break;
        }
    }

    // FUNGSI LINK ANIMATOR: Berfungsi untuk membuka kunci status 'isBusy'
    public void FinishAction()
    {
        isBusy = false;
        Debug.Log("Serangan Selesai, Bos kembali bergerak.");
    }
}