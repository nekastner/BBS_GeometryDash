using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    private InputAction _jumpAction;

    private Rigidbody2D _body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _body = new Rigidbody2D();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_jumpAction.WasPerformedThisFrame())
        {
            _body.AddForceY(1);
        }
    }
}
