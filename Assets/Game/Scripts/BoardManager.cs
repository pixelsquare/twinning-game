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

        public event UnityAction<GameData> OnGameDataUpdated;

        private BoardController _boardController;
        private GameData _gameData;

        private void Awake()
        {
            _gameData = SaveManager.Instance.Load();
            var boardSize = GameConfig.Instance.BoardSize;
            var boardData = _gameData.boardGameData ?? new BoardGameData(boardSize);
            _boardController = new BoardController(boardData, _boardGameView);
            _gameData.boardGameData = boardData;
        }

        private void OnEnable()
        {
            _boardController.OnCardMatched += HandleCardMatched;
            _boardGameView.OnCardAnimFinished += HandleCardAnimFinished;
        }

        private void OnDisable()
        {
            _boardController.OnCardMatched -= HandleCardMatched;
            _boardGameView.OnCardAnimFinished -= HandleCardAnimFinished;
        }

        private void Start()
        {
            SaveGameData();
        }

        private void AddTurnCount(uint turn = 1)
        {
            _gameData.turns += turn;
        }

        private void AddMatchCount(uint match = 1)
        {
            _gameData.matches += match;
        }

        private void SaveGameData()
        {
            SaveManager.Instance.Save(_gameData);
            OnGameDataUpdated?.Invoke(_gameData);
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
