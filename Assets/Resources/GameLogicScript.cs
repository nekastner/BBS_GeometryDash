using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Resources
{
    public class LogicScript : MonoBehaviour
    {
        [SerializeField] private InputManagementScript ims;
        
        public GameObject startScreen;
        public GameObject gameOverScreen;

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI highScoreText;
    
        private GameState _gameState;

        private int _score;
        private int _highScore;

        public void Start()
        {
            this.ims.Controls.Player.Jump.performed += this.StartGame;
            Time.timeScale = 0;
            
            this.gameOverScreen.SetActive(false);
            this.startScreen.SetActive(true);
        }
        
        public void AddScore(int scoreToAdd)
        {
            this._score += scoreToAdd;
            this.scoreText.text = this._score.ToString();
        
            if (this._score > this._highScore) this.UpdateHighScore(this._score);
        }

        private void UpdateHighScore(int newHighScore)
        {
            this._highScore = newHighScore;

            this.highScoreText.text = this._highScore.ToString();

            if (PlayerPrefs.GetInt("HighScore") >= this._highScore) return;

            PlayerPrefs.SetInt("HighScore", this._highScore);
            PlayerPrefs.Save();
        }

        public bool IsGameOver => this._gameState == GameState.GameOver;

        private void StartGame(InputAction.CallbackContext ctx)
        {
            switch (this._gameState)
            {
                case GameState.Playing:
                    return;
                
                case GameState.NotStarted:
                    this._gameState = GameState.Playing;
                    this.gameOverScreen.SetActive(false);
                    this.startScreen.SetActive(false);
                    Time.timeScale = 1;
                    break;
                
                case  GameState.GameOver:
                    this.UnstartGame();
                    break;
            }
        }
    
        private void UnstartGame()
        {
            this._gameState = GameState.NotStarted;
            Time.timeScale = 0;
            
            this.gameOverScreen.SetActive(false);
            this.startScreen.SetActive(true);
        }

        public void GameOver()
        {
            this._gameState = GameState.GameOver;
            this.gameOverScreen.SetActive(true);
        }

        public void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        public void Quit() => Application.Quit();
    }

    public enum GameState
    {
        NotStarted,
        Playing,
        GameOver
    }
}
