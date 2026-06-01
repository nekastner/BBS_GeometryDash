using System.Collections;
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
        private bool _isRotating;
    
        public void Start()
        {
            this.ims.Controls.Player.Jump.performed += this.Jump;
        }

        private void Jump(InputAction.CallbackContext ctx)
        {
            if (this.logic.IsGameOver || (!this._hasContact && this._airJumpsLeft == 0)) return;
            
            this.rb.linearVelocity = Vector2.up * this.jumpForce;
            this.StartCoroutine(this.Rotate(90f, 1.0f));
            
            this._hasContact = false;
            if (this._airJumpsLeft != 0) this._airJumpsLeft--;
        }

        private void PowerJump()
        {
            this.rb.linearVelocity = Vector2.up * this.jumpForce * this.powerJumpForceMultiplier;
            this._airJumpsLeft = 1;
            this.StartCoroutine(this.Rotate(90f, 2.0f));
        }

        private void OnCollisionEnter2D (Collision2D collision) => this.OnTriggerEnter2D(collision.collider);
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                this._hasContact = true;
                this.StopRotation();
            }
            
            if (other.gameObject.CompareTag("GameOver"))
            {
                this.logic.GameOver();
            }

            if (other.gameObject.CompareTag("PowerJump"))
            {
                this.PowerJump();
            }

            if (other.gameObject.CompareTag("ScoreInc"))
            {
                this.logic.AddScore(1);
            }
        }
        
        private IEnumerator Rotate(float angle, float duration)
        {
            this._isRotating = true;

            var startRotation = this.rb.transform.rotation;
            var endRotation = startRotation * Quaternion.Euler(0, 0, -angle);
        
            var elapsed = 0f;

            while (elapsed < duration && this._isRotating)
            {
                elapsed += Time.deltaTime;
                this.rb.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
                yield return null;
            }

            this.rb.transform.rotation = endRotation;

            this._isRotating = false;
        }
        
        private void StopRotation() => this._isRotating = false;
    }
}
