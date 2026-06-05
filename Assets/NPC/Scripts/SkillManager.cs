using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public PlayerController player;

    public void ChooseShadow()
    {
        player.canDashUnlocked = true;

        Debug.Log("Skill Shadow (Dash) diperoleh!");
    }

    public void ChooseWind()
    {
        player.canDoubleJumpUnlocked = true;

        Debug.Log("Skill Wind (Double Jump) diperoleh!");
    }
}