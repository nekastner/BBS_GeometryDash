using UnityEngine;

public class InputManagementScript : MonoBehaviour
{
    public static InputManagementScript Instance { get; private set; }
    
    public Player Player { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Player = new Player();
        //Player.Enable();
    }
}