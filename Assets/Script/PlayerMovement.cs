using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Kecepatan gerak player
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        // Mengambil komponen Rigidbody2D yang ada di kapsul
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Mendapatkan input dari keyboard (WASD atau Panah)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Menggerakkan kapsul berdasarkan input keyboard
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }
}