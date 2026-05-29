using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    
    [Tooltip("Semakin mendekati 1, objek semakin mengikuti kamera (terasa sangat jauh). Jika 0, objek diam di dunia.")]
    [Range(0f, 1f)]
    public float parallaxFactorX;
    public float parallaxFactorY; // Opsional jika game kamu bisa melompat tinggi

    void Start()
    {
        // Mengambil posisi kamera utama
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        // Menghitung seberapa jauh kamera telah bergerak
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        
        // Menggerakkan background berdasarkan pergerakan kamera dikali faktor parallax
        transform.position += new Vector3(deltaMovement.x * parallaxFactorX, deltaMovement.y * parallaxFactorY, 0);
        
        // Memperbarui posisi terakhir kamera
        lastCameraPosition = cameraTransform.position;
    }
}