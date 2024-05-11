using UnityEngine;
using UnityEngine.Events;

namespace PxlSq.Game
{
    /// <summary>
    /// Responsible for managing the game board.
    /// </summary>
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private BoardGameView _boardGameView;
        [SerializeField] private AudioManager _audioManager;

        public static event UnityAction<bool> OnCardMatched;

        private void Awake()
        {
            var gameData = GameDataManager.Instance.GameData;
            var boardSize = GameConfig.Instance.BoardSize;
            var boardData = gameData?.boardGameData ?? new BoardGameData(boardSize);
            _ = new BoardController(boardData, _boardGameView);
            gameData.boardGameData = boardData;
        }

        private void OnEnable()
        {
            BoardController.OnCardMatched += HandleCardMatched;
            BoardGameView.OnCardAnimFinished += HandleCardAnimFinished;
        }

        private void OnDisable()
        {
            BoardController.OnCardMatched -= HandleCardMatched;
            BoardGameView.OnCardAnimFinished -= HandleCardAnimFinished;
        }

        private void Start()
        {
            SaveGameData();
        }

        private void AddTurnCount(uint turn = 1)
        {
            GameDataManager.Instance.Turns += turn;
        }

        private void AddMatchCount(uint match = 1)
        {
            GameDataManager.Instance.Matches += match;
        }

        private void SaveGameData()
        {
            GameDataManager.Instance.SaveGameData();
        }

        private void HandleCardMatched(bool didMatch)
        {
            AddTurnCount();

            if (didMatch)
            {
                AddMatchCount();
                _audioManager.PlaySfx(SfxType.CardMatch);
            }
            else
            {
                _audioManager.PlaySfx(SfxType.CardMisMatchSfx);
            }

            SaveGameData();
            OnCardMatched?.Invoke(didMatch);
        }

        private void HandleCardAnimFinished(Card card)
        {
            if (card.IsShown)
            {
                _audioManager.PlaySfx(SfxType.CardFlip);
            }
        }
    }
}
