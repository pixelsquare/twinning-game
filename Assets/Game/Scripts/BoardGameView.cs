using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PxlSq.Game
{
    /// <summary>
    /// Board Game View
    /// </summary>
    public class BoardGameView : MonoBehaviour
    {
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private GameObject _cardListViewPrefab;
        [SerializeField] private Transform _cardGroupViewPrefab;
        [SerializeField] private Sprite[] _cardLogos;

        public static event UnityAction<Card> OnCardSelected = null;
        public static event UnityAction<Card> OnCardAnimFinished = null;

        private List<Card> _cards = new List<Card>();
        private Transform _cardGroupViewTransform;

        /// <summary>
        /// Populates the game board.
        /// </summary>
        /// <param name="boardSize">Board width and height</param>
        /// <exception cref="ArgumentException">Throws an exception when the total cards is odd</exception>
        public void PopulateGameBoard(BoardGameData gameData)
        {
            var boardSize = gameData.boardSize;
            var cardCount = boardSize.TotalCount;

            if (cardCount % 2 == 1)
            {
                throw new ArgumentException("Invalid argument! Card count must be even.", nameof(boardSize));
            }

            _cards.Clear();
            _cardGroupViewTransform = Instantiate(_cardGroupViewPrefab, transform);

            var idx = 0;

            for (var y = 0; y < boardSize.height; y++)
            {
                var cardListView = Instantiate(_cardListViewPrefab, _cardGroupViewTransform);

                for (var x = 0; x < boardSize.width; x++)
                {
                    var btnIdx = idx;
                    var cardId = gameData.cardIds[btnIdx];
                    var cardLogo = GetCardLogo(cardId);
                    var card = Instantiate(_cardPrefab, cardListView.transform);
                    card.Initialize(btnIdx, cardLogo, HandleCardClicked);
                    card.OnAnimationFinished += HandleCardAnimFinished;
                    card.name = $"Card_{btnIdx}";
                    _cards.Add(card);
                    idx++;
                }
            }

            StartCoroutine(UpdateBoardRoutine(gameData));
        }

        private Sprite GetCardLogo(int cardId)
        {
            if (cardId < 0 || cardId >= _cardLogos.Length)
            {
                return null;
            }

            return _cardLogos[cardId];
        }

        private void DiscardInvalidCards(BoardGameData gameData)
        {
            foreach (var card in _cards)
            {
                var cardId = gameData.cardIds[card.Index];
                card.gameObject.SetActive(cardId >= 0);
            }
        }

        /// <summary>
        /// Handles the any card click event.
        /// </summary>
        /// <param name="card"></param>
        private void HandleCardClicked(Card card)
        {
            OnCardSelected?.Invoke(card);
        }

        private void HandleCardAnimFinished(Card card)
        {
            OnCardAnimFinished?.Invoke(card);
        }

        private IEnumerator UpdateBoardRoutine(BoardGameData gameData)
        {
            yield return StartCoroutine(RemoveAllLayoutGroupsRoutine());
            DiscardInvalidCards(gameData);
        }

        /// <summary>
        /// Removes all layout groups after a frame.
        /// Ensures that the UI elements has been properly positioned
        /// And prevents the canvas runtime layout calculation.
        /// </summary>
        /// <returns></returns>
        private IEnumerator RemoveAllLayoutGroupsRoutine()
        {
            yield return null;
            var layoutGroups = transform.GetComponentsInChildren<LayoutGroup>();
            
            foreach (var layoutGroup in layoutGroups)
            {
                Destroy(layoutGroup);
            }
        }
    }
}
