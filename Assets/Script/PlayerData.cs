using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public bool hasDash = false;
    public bool hasDoubleJump = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}