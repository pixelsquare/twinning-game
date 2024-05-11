using UnityEngine;

namespace PxlSq.Game
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private BoardManager _boardManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private UIView _uiView;

        private void OnEnable()
        {
            _uiView.OnBackButtonClicked += HandleBackButtonClicked;
            _boardManager.OnGameDataUpdated += HandleCardMatched;
        }

        private void OnDisable()
        {
            _uiView.OnBackButtonClicked -= HandleBackButtonClicked;
            _boardManager.OnGameDataUpdated -= HandleCardMatched;
        }

        private void HandleCardMatched(GameData gameData)
        {
            _uiView.UpdateTurnCount(gameData.turns);
            _uiView.UpdateMatchCount(gameData.matches);
        }

        private void HandleBackButtonClicked()
        {
            _audioManager.PlaySfx(SfxType.GameOver);
        }
    }
}
