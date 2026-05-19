using UnityEngine;

public class InputManagementScript : MonoBehaviour
{
    public static InputManagementScript Instance { get; private set; }
    
    public Controls Controls { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.Controls = new Controls();
        this.Controls.Enable();
    }
}
