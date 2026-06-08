using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialDialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public GameObject controlImage; 
    public Button nextButton;

    private int step = 0;

    // 1. Awake ditaruh di sini (terpisah dari fungsi lain)
    void Awake()
    {
        // Menggunakan Awake memastikan kode ini jalan duluan begitu Canvas aktif
        ShowTutorialStep();
        nextButton.onClick.AddListener(NextStep);
    }

    // 2. Fungsi ShowTutorialStep berdiri sendiri
    void ShowTutorialStep()
    {
        if (step == 0)
        {
            dialogText.text = "Halo! Selamat datang di dunia ini. Aku akan memandumu.";
            controlImage.SetActive(false); 
        }
        else if (step == 1)
        {
            dialogText.text = "Gunakan tombol berikut untuk bergerak dan menjelajah.";
            controlImage.SetActive(true); 
        }
        else if (step == 2)
        {
            dialogText.text = "Bagus! Sekarang kamu sudah siap. Petualangan dimulai!";
            controlImage.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false); 
        }
    } // Kurung penutup ShowTutorialStep di sini

    // 3. Fungsi NextStep juga berdiri sendiri
    public void NextStep()
    {
        step++;
        ShowTutorialStep();
    }
}