using UnityEngine;

public class BossFollowPlayer : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform playerTransform;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float stoppingDistance = 2f;

    [Header("Attack Settings")]
    public float attackCooldown = 2f;
    private float nextAttackTime = 0f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

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
        if (playerTransform == null || animator == null) return;

        // Ambil info state animasi saat ini
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // JIKA SEDANG MENYERANG: Kunci posisi fisik agar tidak bergeser, lari, atau memicu attack berulang
        if (stateInfo.IsName("basicAttack"))
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // KONDISI 1: Jarak jauh -> Kejar Player
        if (distanceToPlayer > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
        // KONDISI 2: Jarak dekat -> Berhenti dan Menyerang
        else
        {
            animator.SetBool("isRunning", false);

            // Cek apakah cooldown serangan sudah selesai
            if (Time.time >= nextAttackTime)
            {
                // Eksekusi trigger pemicu serangan "Attack" secara paksa dari script utama
                animator.SetTrigger("Attack");

                // Setel waktu tunggu cooldown berikutnya
                nextAttackTime = Time.time + attackCooldown;
            }
        }

        FlipTowardsPlayer();
    }

    // Fungsi Animation Event (Bisa ditempel di frame tebasan kapak)
    public void BeriDamageKePlayer()
    {
        float arahHadap = spriteRenderer.flipX ? -1f : 1f;
        Vector2 posisiSerang = (Vector2)transform.position + new Vector2(arahHadap * 0.8f, 0f);
        float radiusSerang = 1f;

        Collider2D hitPlayer = Physics2D.OverlapCircle(posisiSerang, radiusSerang, LayerMask.GetMask("Player"));

        if (hitPlayer != null)
        {
            Debug.Log("BOOM! Player terkena tebasan kapak Boss!");
        }
    }

    void FlipTowardsPlayer()
    {
        if (playerTransform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerTransform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (spriteRenderer == null) return;
        float arahHadap = spriteRenderer.flipX ? -1f : 1f;
        Vector2 posisiSerang = (Vector2)transform.position + new Vector2(arahHadap * 0.8f, 0.8f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posisiSerang, 1.2f);
    }
}