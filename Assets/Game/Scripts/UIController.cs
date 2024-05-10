using UnityEngine;

namespace PxlSq.Game
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private BoardManager _boardManager;
        [SerializeField] private UIView _uiView;

        private void OnEnable()
        {
            _boardManager.OnGameDataUpdated += HandleCardMatched;
        }

        private void OnDisable()
        {
            _boardManager.OnGameDataUpdated -= HandleCardMatched;
        }

        private void HandleCardMatched(GameData gameData)
        {
            _uiView.UpdateTurnCount(gameData.turns);
            _uiView.UpdateMatchCount(gameData.matches);
        }
    }
}
