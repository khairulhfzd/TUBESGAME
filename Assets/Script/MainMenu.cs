using UnityEngine;
using UnityEngine.SceneManagement; // Wajib digunakan untuk mengatur perpindahan Scene

public class MainMenu : MonoBehaviour
{
    [Header("Pengaturan Scene")]
    [Tooltip("Masukkan nama file Scene gameplay (hutan/gua) kamu di sini. Harus sama persis huruf besar-kecilnya.")]
    [SerializeField] private string namaSceneGameplay = "Level1";

    /// <summary>
    /// Fungsi untuk tombol START
    /// </summary>
    public void PlayGame()
    {
        // Memuat scene permainan utama berdasarkan nama yang diisi di Inspector
        SceneManager.LoadScene(namaSceneGameplay);
    }

    /// <summary>
    /// Fungsi untuk tombol EXIT
    /// </summary>
    public void QuitGame()
    {
        // Pesan konfirmasi di Console saat dijalankan di dalam Editor Unity
        Debug.Log("Tombol EXIT diklik. Game berhasil ditutup!");

        // Perintah untuk menutup aplikasi game (aktif saat game sudah di-build menjadi .exe/.apk)
        Application.Quit();
    }
}