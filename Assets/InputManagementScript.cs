using UnityEngine;

public class InputManagementScript : MonoBehaviour
{
    public static InputManagementScript Instance { get; private set; }
    
    public Controls Controls { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Controls = new Controls();
        Controls.Enable();
    }
}
