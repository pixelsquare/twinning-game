using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PxlSq.Game
{
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

        public void SetMenuPanelActive(bool active)
        {
            _menuPanel.SetActive(active);
        }

        public void SetGamePanelActive(bool active)
        {
            _gamePanel.SetActive(active);
        }

        public void SetStartButtonActive(bool active)
        {
            _startButton.gameObject.SetActive(active);
        }

        public void SetContinueButtonActive(bool active)
        {
            _continueButton.gameObject.SetActive(active);
        }

        public void UpdateHighScore(uint highScore)
        {
            _highScoreText.text = $"High Score\n{highScore}";
        }

        public void UpdateTurnCount(uint turnCount)
        {
            _turnCountText.text = $"Turns: {turnCount}";
        }

        public void UpdateMatchCount(uint matchCount)
        {
            _matchCountText.text = $"Matches: {matchCount}";
        }

        public void UpdateScoreCount(uint scoreCount)
        {
            _scoreCountText.text = $"Score: {scoreCount}";
        }
    }
}
