using UnityEngine;
using UnityEngine.Events;

namespace PxlSq.Game
{
    /// <summary>
    /// Handles the game board
    /// </summary>
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private BoardGameView _boardGameView;
        [SerializeField] private AudioManager _audioManager;

        public static event UnityAction<bool> OnCardMatched;

        private BoardController _boardController;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += HandleGameStateChanged;
            BoardController.OnCardMatched += HandleCardMatched;
            BoardGameView.OnCardAnimFinished += HandleCardAnimFinished;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= HandleGameStateChanged;
            BoardController.OnCardMatched -= HandleCardMatched;
            BoardGameView.OnCardAnimFinished -= HandleCardAnimFinished;
        }

        /// <summary>
        /// Initializes the board manager
        /// </summary>
        private void Initialize()
        {
            if (!_gameManager.IsGameContinued)
            {
                GameDataManager.Instance.ResetGameData();
            }

            var gameData = GameDataManager.Instance.GameData;
            var boardSize = GameConfig.Instance.BoardSize;
            var boardData = _gameManager.IsGameContinued 
                            ? gameData?.boardGameData 
                            : new BoardGameData(boardSize);
            _boardController ??= new BoardController(boardData, _boardGameView);
            _boardController.Initialize(boardData);
            gameData.boardGameData = boardData;
        }

        /// <summary>
        /// Adds a turn count
        /// </summary>
        /// <param name="turn"></param>
        private void AddTurnCount(uint turn = 1)
        {
            GameDataManager.Instance.Turns += turn;
        }

        /// <summary>
        /// Adds a match count
        /// </summary>
        /// <param name="match"></param>
        private void AddMatchCount(uint match = 1)
        {
            GameDataManager.Instance.Matches += match;
        }

        /// <summary>
        /// Writes the game data to storage
        /// </summary>
        private void SaveGameData()
        {
            GameDataManager.Instance.SaveGameData();
        }

        /// <summary>
        /// Resets the game data and changes the state if game won
        /// </summary>
        private void CheckGameWon()
        {
            var didWin = GameDataManager.Instance.didWin;

            if (didWin)
            {
                GameDataManager.Instance.ResetGameData();
                _gameManager.SetGameState(GameState.Menu);
            }
        }

        /// <summary>
        /// Resets the game board
        /// </summary>
        private void ResetGameBoard()
        {
            var groupViewTransform = transform.GetChild(0);
            Destroy(groupViewTransform.gameObject);
        }

        /// <summary>
        /// Handles game state change event
        /// </summary>
        /// <param name="gameState"></param>
        private void HandleGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Menu:
                {
                    ResetGameBoard();
                    break;
                }
                case GameState.Game:
                {
                    Initialize();
                    SaveGameData();
                    break;
                }
            }
        }

        /// <summary>
        /// Handles card match events
        /// </summary>
        /// <param name="didMatch"></param>
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
            CheckGameWon();
        }

        /// <summary>
        /// Handles the card animation finish event
        /// Plays an sfx on card flipped front facing
        /// </summary>
        /// <param name="card"></param>
        private void HandleCardAnimFinished(Card card)
        {
            if (card.IsShown)
            {
                _audioManager.PlaySfx(SfxType.CardFlip);
            }
        }
    }
}
