using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Resources
{
    public class LogicScript : MonoBehaviour
    {
        [SerializeField] private InputManagementScript ims;
    
        private bool _gameStarted;
        private bool _gamePaused;
        private bool _gameOver;

        private int _score;
        private int _highScore;

        public void Start()
        {
            // TODO: subscribe to input system
            this.ims.Controls.Player.Jump.performed += this.StartGame;
            Time.timeScale = 0;
        }

        public bool GameIsOver()
        {
            return this._gameOver;
        }

        private void StartGame(InputAction.CallbackContext ctx)
        {
            if (this._gameStarted) return;
            this._gameStarted = true;
            // TODO: unsubscribe from input system
            Time.timeScale = 1;
        }
    
        private void UnstartGame()
        {
            this._gameStarted = false;
            Time.timeScale = 0;
            // TODO: set home screen
        }

        private void TogglePause(InputAction.CallbackContext context)
        {
            if (this._gamePaused)
                this.UnpauseGame();
            else
                this.PauseGame();
        }

        private void PauseGame()
        {
            this._gamePaused = true;
            Time.timeScale = 0;
            // TODO: set pause screen
        }

        public void UnpauseGame()
        {
            this._gamePaused = false;
            Time.timeScale = 1;
            // TODO: set game screen

            if (this._gameOver) this.GameOver();
            if (!this._gameStarted) this.UnstartGame();
        }

        public void GameOver()
        {
            this._gameOver = true;
            // TODO: set game over screen
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
