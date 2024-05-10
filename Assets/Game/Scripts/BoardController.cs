using UnityEngine;
using UnityEngine.Events;

namespace PxlSq.Game
{
    /// <summary>
    /// Controls the model and view of the game board.
    /// </summary>
    public class BoardController
    {
        [SerializeField] private BoardGameData _gameData;
        [SerializeField] private BoardGameView _gameView;

        public event UnityAction<bool> OnCardMatched = null;

        private Card _selectedCard = null;
        private readonly System.Random _rng = new System.Random();

        public BoardController(BoardGameData gameData, BoardGameView gameView)
        {
            _gameData = gameData;
            _gameView = gameView;

            // Generate new card ids if we don't have anything saved.
            if (gameData.cardIds == null || gameData.cardIds.Length == 0)
            {
                GenerateCardIds(_gameData.boardSize);
            }

            _gameView.PopulateGameBoard(_gameData);

            _gameView.OnCardSelected += HandleCardSelected;
        }

        ~BoardController()
        {
            _gameView.OnCardSelected -= HandleCardSelected;
        }

        /// <summary>
        /// Generates card pair ids.
        /// </summary>
        /// <param name="boardSize">Board width and height</param>
        public void GenerateCardIds(BoardSize boardSize)
        {
            var cardCount = boardSize.TotalCount;
            _gameData.cardIds = new int[cardCount];

            for (var i = 0; i < cardCount; i++)
            {
                var idx = i % (int)(cardCount / 2);
                _gameData.cardIds[i] = idx;
            }

            ShuffleCards(_gameData.cardIds);
            // PrintCardIds(_gameData.cardIds);
        }

        /// <summary>
        /// Shuffles the card ids.
        /// </summary>
        /// <param name="cardIds">Card Ids</param>
        private void ShuffleCards(int[] cardIds)
        {
            var len = cardIds.Length;
            
            while (len > 1)
            {
                var idx = _rng.Next(len--);
                var temp = cardIds[len];
                cardIds[len] = cardIds[idx];
                cardIds[idx] = temp;
            }
        }

        /// <summary>
        /// Handles card selected event.
        /// </summary>
        /// <param name="card">Current card selected</param>
        private void HandleCardSelected(Card card)
        {
            if (_selectedCard == null)
            {
                _selectedCard = card;
                return;
            }

            // Card match is already determined before animation finishes.
            var isMatched = IsCardMatched(_selectedCard, card);
            _ = new CardMatch(_selectedCard, card, isMatched)
            {
                OnCardMatched = OnCardMatched
            };

            _selectedCard = null;
            
        }

        /// <summary>
        /// Converts card index to card id.
        /// </summary>
        /// <param name="cardIndex">Card index</param>
        /// <returns>Card Id</returns>
        private int GetCardId(int cardIndex)
        {
            return _gameData.cardIds[cardIndex];
        }

        /// <summary>
        /// Checks whether card pair matched.
        /// </summary>
        /// <param name="card1">Card 1</param>
        /// <param name="card2">Card 2</param>
        /// <returns>Returns true if cards matched</returns>
        private bool IsCardMatched(Card card1, Card card2)
        {
            return GetCardId(card1.Index) == GetCardId(card2.Index);
        }

        /// <summary>
        /// For debug purposes.
        /// Prints the card ids in the console.
        /// </summary>
        /// <param name="cardIds">Card Ids</param>
        private void PrintCardIds(int[] cardIds)
        {
            for (var i = 0; i < cardIds.Length; i++)
            {
                Debug.Log($"{i} | {cardIds[i]}");
            }
        }
    }
}
