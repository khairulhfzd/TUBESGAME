using UnityEngine;
using TMPro;

public class GuideDialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    public GameObject dialogPanel;
    public GameObject questionPanel;

    [TextArea]
    public string[] dialogs;

    private int currentDialog = 0;

    void Start()
    {
        if (dialogs.Length > 0)
        {
            dialogText.text = dialogs[0];
        }

        if (questionPanel != null)
        {
            questionPanel.SetActive(false);
        }
    }

    public void NextDialog()
    {
        currentDialog++;

        if (currentDialog < dialogs.Length)
        {
            dialogText.text = dialogs[currentDialog];
        }
        else
        {
            if (dialogPanel != null)
            {
                dialogPanel.SetActive(false);
            }

            if (questionPanel != null)
            {
                questionPanel.SetActive(true);
            }
        }
    }
}