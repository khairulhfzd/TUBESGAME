using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    private float moveInput;

    [Header("Skills (Unlocked via Math)")]
    public bool canDoubleJumpUnlocked = false;
    public bool canDashUnlocked = false;
    private bool canDoubleJump;

    [Header("Dash Settings")]
    private bool isDashing;
    public float dashPower = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;
    private bool canDash = true;

    // --- FITUR BARU: COMBAT ---
    [Header("Combat Settings")]
    public float attackRate = 2f; // Kecepatan serangan (bisa 2 kali per detik)
    private float nextAttackTime = 0f;

    [Header("Components")]
    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    public GameObject visual;

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = visual.GetComponent<Animator>();
    }

    void Update()
    {
        if (isDashing) return;

        moveInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Perintah Animasi Lari
        if (moveInput != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        // Perintah Animasi Lompat & Jatuh
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        if (isGrounded) canDoubleJump = true;

        // Input Lompat
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (canDoubleJumpUnlocked && canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }

        // Input Dash (Shift Kiri)
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && canDashUnlocked)
        {
            StartCoroutine(Dash());
        }

        // --- INPUT SERANGAN (Klik Kiri atau Tombol J) ---
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate; // Mengatur jeda serangan
            }
        }

        Flip();
    }

    void FixedUpdate()
    {
        if (isDashing) return;
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void Attack()
    {
        // Memicu parameter Trigger di Animator
        anim.SetTrigger("attack");

        // Cek log di Console untuk memastikan fungsi terpanggil
        Debug.Log("Ninja Menyerang!");
    }

    void Flip()
    {
        // Ambil ukuran visual saat ini
        Vector3 currentScale = visual.transform.localScale;

        // Balikkan hanya nilai X-nya, biarkan Y dan Z tetap seperti aslinya
        if (moveInput > 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        else if (moveInput < 0)
        {
            currentScale.x = -Mathf.Abs(currentScale.x);
        }

        // Terapkan ukuran yang sudah di-update
        visual.transform.localScale = currentScale;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(visual.transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }
    }
}