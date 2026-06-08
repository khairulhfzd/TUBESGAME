using UnityEngine;
using TMPro;

public class DialogManager0 : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    [TextArea]
    public string[] dialogs;

    private int currentDialog = 0;

    void Start()
    {
        dialogText.text = dialogs[0];
    }

    public void NextDialog()
    {
        currentDialog++;

        if (currentDialog < dialogs.Length)
        {
            dialogText.text = dialogs[currentDialog];
        }
    }
}