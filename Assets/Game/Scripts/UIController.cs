using UnityEngine;

namespace PxlSq.Game
{
    /// <summary>
    /// Controls the user interface
    /// </summary>
    public class UIController : MonoBehaviour
    {
        [SerializeField] private BoardManager _boardManager;
        [SerializeField] private UIView _uiView;

        /// <summary>
        /// Sets the main panel active and disables the game panel
        /// </summary>
        /// <param name="active"></param>
        public void SetMainPanelActive(bool active)
        {
            UpdateMainUI();
            _uiView.SetMenuPanelActive(active);
            _uiView.SetGamePanelActive(!active);
        }

        /// <summary>
        /// Sets the game panel active and disables the game panel
        /// </summary>
        /// <param name="active"></param>
        public void SetGamePanelActive(bool active)
        {
            UpdateGameUI();
            _uiView.SetGamePanelActive(active);
            _uiView.SetMenuPanelActive(!active);
        }

        private void OnEnable()
        {
            GameDataManager.OnGameDataUpdated += HandleCardMatched;
            GameManager.OnGameStateChanged += HandleGameStateChanged;
        }

        private void OnDisable()
        {
            GameDataManager.OnGameDataUpdated -= HandleCardMatched;
            GameManager.OnGameStateChanged -= HandleGameStateChanged;
        }

        private void Start()
        {
            UpdateMainUI();
        }

        /// <summary>
        /// Handles the card match and updates ui contents
        /// </summary>
        /// <param name="gameData"></param>
        private void HandleCardMatched(GameData gameData)
        {
            _uiView.UpdateTurnCount(gameData.turns);
            _uiView.UpdateMatchCount(gameData.matches);
            _uiView.UpdateScoreCount(gameData.score);
        }

        /// <summary>
        /// Handles the game state change and activates the necessary panel
        /// </summary>
        /// <param name="gameState"></param>
        private void HandleGameStateChanged(GameState gameState)
        {
            SetMainPanelActive(gameState == GameState.Menu);
            SetGamePanelActive(gameState == GameState.Game);
        }

        /// <summary>
        /// Updates the main user interface
        /// </summary>
        private void UpdateMainUI()
        {
            _uiView.UpdateHighScore(SaveManager.Instance.GameData.highscore);
            _uiView.SetContinueButtonActive(GameDataManager.Instance.HasExistingGameData);
        }

        /// <summary>
        /// Updates the game user interface
        /// </summary>
        private void UpdateGameUI()
        {
            HandleCardMatched(GameDataManager.Instance.GameData);
        }
    }
}
