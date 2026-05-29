using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTeleport : MonoBehaviour
{
    [Header("Pengaturan Scene")]
    [SerializeField] private string targetSceneName = "HutanBambu(MiniBoss)"; 
    
    [Header("Komponen UI Animasi")]
    public Animator fadeAnimator; // Tarik UI Panel Hitam ke sini
    public float fadeDuration = 1f; // Durasi animasi fade out

    private bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Memastikan hanya Player yang bisa memicu teleportasi, dan tidak mendobel proses
        if (other.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(StartTransition());
        }
    }

    IEnumerator StartTransition()
    {
        isTeleporting = true;

        // 1. Jalankan animasi fade in (layar menjadi hitam)
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("StartFade");
        }

        // 2. Tunggu sampai layar benar-benar hitam pekat
        yield return new WaitForSeconds(fadeDuration);

        // 3. Pindah ke scene Mini Boss
        SceneManager.LoadScene(targetSceneName);
    }
}