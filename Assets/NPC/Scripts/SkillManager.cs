using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public PlayerController player;

    public GameObject skillChoisePanel;

    private void Awake()
    {
        // If player is not assigned in the Inspector, try to find it
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
    }

    public void ChooseShadow()
    {
        if (player == null)
        {
            Debug.LogError("PlayerController not found! Make sure it's assigned in the Inspector or exists in the scene.");
            return;
        }

        player.canDashUnlocked = true;

        Debug.Log("Skill Shadow (Dash) diperoleh!");

        PlayerData.Instance.hasDash = true;

        skillChoisePanel.SetActive(false);
    }

    public void ChooseWind()
    {
        if (player == null)
        {
            Debug.LogError("PlayerController not found! Make sure it's assigned in the Inspector or exists in the scene.");
            return;
        }

        player.canDoubleJumpUnlocked = true;

        Debug.Log("Skill Wind (Double Jump) diperoleh!");
        
        PlayerData.Instance.hasDoubleJump = true;

        skillChoisePanel.SetActive(false);
    }
}