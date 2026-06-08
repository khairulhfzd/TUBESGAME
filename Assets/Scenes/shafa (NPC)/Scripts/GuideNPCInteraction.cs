using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GuideNPCInteraction : MonoBehaviour
{
    public GameObject interactText;
    public GameObject tutorialCanvas;

    public TextMeshProUGUI dialogText; // Tambahkan ini

    [Header("NPC Sprite Settings")]
    public SpriteRenderer npcSprite;
    public Sprite normalSprite;
    public Sprite talkSprite;

    private bool isPlayerNearby;

    void Start()
    {
        if (interactText != null)
            interactText.SetActive(false);

        if (tutorialCanvas != null)
            tutorialCanvas.SetActive(false);

        if (npcSprite != null && normalSprite != null)
            npcSprite.sprite = normalSprite;
    }

    void Update()
    {
        if (isPlayerNearby)
        {
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (tutorialCanvas != null)
                    tutorialCanvas.SetActive(true);

                // Paksa DialogText aktif
                if (dialogText != null)
                    dialogText.gameObject.SetActive(true);

                if (interactText != null)
                    interactText.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            if (interactText != null)
                interactText.SetActive(true);

            if (npcSprite != null && talkSprite != null)
                npcSprite.sprite = talkSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            if (interactText != null)
                interactText.SetActive(false);

            if (tutorialCanvas != null)
                tutorialCanvas.SetActive(false);

            if (npcSprite != null && normalSprite != null)
                npcSprite.sprite = normalSprite;
        }
    }
}