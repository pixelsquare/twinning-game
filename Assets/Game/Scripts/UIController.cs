using UnityEngine;

namespace PxlSq.Game
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private BoardManager _boardManager;
        [SerializeField] private UIView _uiView;

        public void SetMainPanelActive(bool active)
        {
            UpdateMainUI();
            _uiView.SetMenuPanelActive(active);
            _uiView.SetGamePanelActive(!active);
        }

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

        private void HandleCardMatched(GameData gameData)
        {
            _uiView.UpdateTurnCount(gameData.turns);
            _uiView.UpdateMatchCount(gameData.matches);
            _uiView.UpdateScoreCount(gameData.score);
        }

        private void HandleGameStateChanged(GameState gameState)
        {
            SetMainPanelActive(gameState == GameState.Menu);
            SetGamePanelActive(gameState == GameState.Game);
        }

        private void UpdateMainUI()
        {
            _uiView.UpdateHighScore(SaveManager.Instance.GameData.highscore);
            _uiView.SetContinueButtonActive(GameDataManager.Instance.HasExistingGameData);
        }

        private void UpdateGameUI()
        {
            HandleCardMatched(GameDataManager.Instance.GameData);
        }
    }
}
