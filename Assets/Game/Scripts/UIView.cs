using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PxlSq.Game
{
    /// <summary>
    /// User interview view
    /// </summary>
    public class UIView : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private TMP_Text _highScoreText;

        [Header("Game")]
        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _turnCountText;
        [SerializeField] private TMP_Text _matchCountText;
        [SerializeField] private TMP_Text _scoreCountText;

        /// <summary>
        /// Sets the menu panel active
        /// </summary>
        /// <param name="active"></param>
        public void SetMenuPanelActive(bool active)
        {
            _menuPanel.SetActive(active);
        }

        /// <summary>
        /// Sets the game panel active.
        /// </summary>
        /// <param name="active"></param>
        public void SetGamePanelActive(bool active)
        {
            _gamePanel.SetActive(active);
        }

        /// <summary>
        /// Sets the start button active.
        /// </summary>
        /// <param name="active"></param>
        public void SetStartButtonActive(bool active)
        {
            _startButton.gameObject.SetActive(active);
        }

        /// <summary>
        /// Sets continue button active.
        /// </summary>
        /// <param name="active"></param>
        public void SetContinueButtonActive(bool active)
        {
            _continueButton.gameObject.SetActive(active);
        }

        /// <summary>
        /// Updates the high score.
        /// </summary>
        /// <param name="highScore"></param>
        public void UpdateHighScore(uint highScore)
        {
            _highScoreText.text = $"High Score\n{highScore}";
        }

        /// <summary>
        /// Updates the turn counter
        /// </summary>
        /// <param name="turnCount"></param>
        public void UpdateTurnCount(uint turnCount)
        {
            _turnCountText.text = $"Turns: {turnCount}";
        }

        /// <summary>
        /// Updates the match counter
        /// </summary>
        /// <param name="matchCount"></param>
        public void UpdateMatchCount(uint matchCount)
        {
            _matchCountText.text = $"Matches: {matchCount}";
        }

        /// <summary>
        /// Updates the score counter
        /// </summary>
        /// <param name="scoreCount"></param>
        public void UpdateScoreCount(uint scoreCount)
        {
            _scoreCountText.text = $"Score: {scoreCount}";
        }
    }
}
