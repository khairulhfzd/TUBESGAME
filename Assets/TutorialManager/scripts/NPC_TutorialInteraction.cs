using UnityEngine;

public class NPC_TutorialInteraction : MonoBehaviour
{
    public GameObject tutorialCanvas;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tutorialCanvas != null)
            {
                tutorialCanvas.SetActive(true);
            }
        }
    }
}