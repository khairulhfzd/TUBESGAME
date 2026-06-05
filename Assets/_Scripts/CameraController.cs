using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Setup")]
    public Transform target; // Slot untuk masukin karaktermu

    [Header("Camera Settings")]
    public float smoothSpeed = 0.125f; // Kecepatan kamera menyusul karakter
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Jarak kamera (Y=2 biar agak ke atas, Z=-10 wajib biar kelihatan)

    void FixedUpdate()
    {
        if (target != null)
        {
            // Menghitung posisi yang dituju kamera
            Vector3 desiredPosition = target.position + offset;

            // Membuat pergerakan kamera jadi halus (nggak kaku)
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Mengubah posisi kamera
            transform.position = smoothedPosition;
        }
    }
}