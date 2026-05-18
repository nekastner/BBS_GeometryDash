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
        
        private bool _hasContact;
    
        public void Start()
        {
            this.ims.Controls.Player.Jump.performed += this.Jump;
        }

        public void Update()
        {
            if (!this.logic.GameIsOver()) return;
            
            this.ims.Controls.Player.Jump.performed -= this.Jump;
        }

        private void Jump(InputAction.CallbackContext ctx)
        {
            if (!this._hasContact) return;
            
            this.rb.linearVelocity = Vector2.up * this.jumpForce;
            this._hasContact = false;
        }

        private void OnCollisionEnter2D (Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("KillOnContact"))
            {
                this.logic.GameOver();
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                this._hasContact = true;
            }
        }
    }
}
