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
            ims.Controls.Player.Jump.performed += StartGame;
            Time.timeScale = 0;
        }

        public bool GameIsOver()
        {
            return _gameOver;
        }

        private void StartGame(InputAction.CallbackContext ctx)
        {
            if (_gameStarted) return;
            _gameStarted = true;
            // TODO: unsubscribe from input system
            Time.timeScale = 1;
        }
    
        private void UnstartGame()
        {
            _gameStarted = false;
            Time.timeScale = 0;
            // TODO: set home screen
        }

        private void TogglePause(InputAction.CallbackContext context)
        {
            if (_gamePaused) UnpauseGame();
            else PauseGame();
        }

        private void PauseGame()
        {
            _gamePaused = true;
            Time.timeScale = 0;
            // TODO: set pause screen
        }

        public void UnpauseGame()
        {
            _gamePaused = false;
            Time.timeScale = 1;
            // TODO: set game screen

            if (_gameOver) GameOver();
            if (!_gameStarted) UnstartGame();
        }

        public void GameOver()
        {
            _gameOver = true;
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
