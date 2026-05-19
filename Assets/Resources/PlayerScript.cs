using UnityEngine;
using UnityEngine.InputSystem;

namespace Resources
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private InputManagementScript ims;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private LogicScript logic;
        public float jumpForce;
        public float powerJumpForceMultiplier;
        
        private bool _hasContact;
        private uint _airJumpsLeft;
    
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
            if (!this._hasContact && this._airJumpsLeft == 0) return;
            
            this.rb.linearVelocity = Vector2.up * this.jumpForce;
            
            this._hasContact = false;
            if (this._airJumpsLeft != 0) this._airJumpsLeft--;
        }

        private void PowerJump()
        {
            this.rb.linearVelocity = Vector2.up * this.jumpForce * this.powerJumpForceMultiplier;
            this._airJumpsLeft = 1;
        }

        private void OnCollisionEnter2D (Collision2D collision)
        {
            OnTriggerEnter2D(collision.collider);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                this._hasContact = true;
            }
            if (other.gameObject.CompareTag("GameOver"))
            {
                this.logic.GameOver();
            }

            if (other.gameObject.CompareTag("PowerJump"))
            {
                this.PowerJump();
            }
        }
    }
}
