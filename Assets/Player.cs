using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    private const float JumpForce = 1;
    public InputAction jumpAction;
    public Rigidbody2D body;
    public PlayerInput playerInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];
        jumpAction.performed += OnJugitmp;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) Jump();
    }

    private void Jump()
    {
        body.AddForceY(JumpForce);
    }
}