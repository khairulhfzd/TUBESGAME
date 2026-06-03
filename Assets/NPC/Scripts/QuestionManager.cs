using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public GameObject resultQuest;
    public GameObject resultQuestWrong;

    public void CorrectAnswer()
    {
        resultQuest.SetActive(true);
        resultQuestWrong.SetActive(false);

        Debug.Log("Jawaban Benar!");
    }

    public void WrongAnswer()
    {
        resultQuestWrong.SetActive(true);
        resultQuest.SetActive(false);

        Debug.Log("Jawaban Salah!");
    }
}