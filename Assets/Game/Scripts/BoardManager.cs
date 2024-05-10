using UnityEngine;
using UnityEngine.Events;

namespace PxlSq.Game
{
    /// <summary>
    /// Responsible for managing the game board.
    /// </summary>
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private BoardSize _boardSize = new (2, 1);
        [SerializeField] private BoardGameView _boardGameView;

        public event UnityAction<GameData> OnGameDataUpdated;

        private BoardController _boardController;
        private GameData _gameData;

        private void Awake()
        {
            _gameData = SaveManager.Instance.Load();
            var boardData = _gameData.boardGameData ?? new BoardGameData(_boardSize);
            _boardController = new BoardController(boardData, _boardGameView);
            _gameData.boardGameData = boardData;
        }

        private void OnEnable()
        {
            _boardController.OnCardMatched += HandleCardMatched;
        }

        private void OnDisable()
        {
            _boardController.OnCardMatched -= HandleCardMatched;
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
            }

            SaveGameData();
        }
    }
}
