using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteraction2 : MonoBehaviour
{
    public GameObject interactText;
    public GameObject dialogPanel;

    public GameObject questionPanel;
    public GameObject resultQuest;
    public GameObject resultQuestWrong;

    // sprite npc
    public SpriteRenderer npcSprite;
    public Sprite normalSprite;
    public Sprite talkSprite;

    private bool isPlayerNearby;

    void Start()
    {
        if (interactText != null)
            interactText.SetActive(false);

        if (dialogPanel != null)
            dialogPanel.SetActive(false);

        if (questionPanel != null)
            questionPanel.SetActive(false);

        if (resultQuest != null)
            resultQuest.SetActive(false);

        if (resultQuestWrong != null)
            resultQuestWrong.SetActive(false);

        if (npcSprite != null && normalSprite != null)
            npcSprite.sprite = normalSprite;
    }

    void Update()
    {
        if (isPlayerNearby)
        {
            if (Keyboard.current != null &&
                Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (dialogPanel != null)
                    dialogPanel.SetActive(true);

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

            if (dialogPanel != null)
                dialogPanel.SetActive(false);

            if (questionPanel != null)
                questionPanel.SetActive(false);

            if (resultQuest != null)
                resultQuest.SetActive(false);

            if (resultQuestWrong != null)
                resultQuestWrong.SetActive(false);

            if (npcSprite != null && normalSprite != null)
                npcSprite.sprite = normalSprite;
        }
    }
}