using UnityEngine;
using UnityEngine.InputSystem;

namespace Resources
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private InputManagementScript ims;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private LogicScript logic;
        public float jumpForce = 1;
    
        public void Start()
        {
            ims.Controls.Player.Jump.performed += Jump;
        }

        public void Update()
        {
            if (logic.GameIsOver())
            {
                ims.Controls.Player.Jump.performed -= Jump;
            }
        }

        private void Jump(InputAction.CallbackContext ctx)
        {
            rb.linearVelocity = Vector2.up * jumpForce;
        }

        private void OnCollisionEnter2D (Collision2D collision)
        {
            //logic.GameOver();
        }
    }
}
