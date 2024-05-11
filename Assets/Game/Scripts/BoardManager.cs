using UnityEngine;
using UnityEngine.Events;

namespace PxlSq.Game
{
    /// <summary>
    /// Responsible for managing the game board.
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

        private void CheckGameWon()
        {
            var didWin = GameDataManager.Instance.didWin;

            if (didWin)
            {
                GameDataManager.Instance.ResetGameData();
                _gameManager.SetGameState(GameState.Menu);
            }
        }

        private void ResetGameBoard()
        {
            var groupViewTransform = transform.GetChild(0);
            Destroy(groupViewTransform.gameObject);
        }

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

        private void HandleCardAnimFinished(Card card)
        {
            if (card.IsShown)
            {
                _audioManager.PlaySfx(SfxType.CardFlip);
            }
        }
    }
}
