using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public GameObject resultQuest;
    public GameObject resultQuestWrong;

    public GameObject nextButton2;
    public GameObject skillChoisePanel;

    void Start()
    {
        resultQuest.SetActive(false);
        resultQuestWrong.SetActive(false);

        if (nextButton2 != null)
            nextButton2.SetActive(false);

        if (skillChoisePanel != null)
            skillChoisePanel.SetActive(false);
    }

    public void CorrectAnswer()
    {
        resultQuest.SetActive(true);
        resultQuestWrong.SetActive(false);

        if (nextButton2 != null)
            nextButton2.SetActive(true);

        Debug.Log("Jawaban Benar!");
    }

    public void WrongAnswer()
    {
        resultQuestWrong.SetActive(true);
        resultQuest.SetActive(false);

        if (nextButton2 != null)
            nextButton2.SetActive(false);

        Debug.Log("Jawaban Salah!");
    }

    public void OpenSkillPanel()
    {
        skillChoisePanel.SetActive(true);
    }
}