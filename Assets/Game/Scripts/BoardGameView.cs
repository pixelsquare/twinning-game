using System;
using System.Collections;
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
        [SerializeField] private Transform _cardGroupViewTransform;
        [SerializeField] private Sprite[] _cardLogos;

        public static event UnityAction<Card> OnCardSelected = null;
        public static event UnityAction<Card> OnCardAnimFinished = null;


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

            var idx = 0;

            for (var y = 0; y < boardSize.height; y++)
            {
                var cardListView = Instantiate(_cardListViewPrefab, _cardGroupViewTransform);

                for (var x = 0; x < boardSize.width; x++)
                {
                    var btnIdx = idx;
                    var cardId = gameData.cardIds[btnIdx];
                    var cardLogo = cardId < _cardLogos.Length ? _cardLogos[cardId] : null;
                    var card = Instantiate(_cardPrefab, cardListView.transform);
                    card.Initialize(btnIdx, cardLogo, HandleCardClicked);
                    card.OnAnimationFinished += HandleCardAnimFinished;
                    idx++;
                }
            }
        }

        private IEnumerator Start()
        {
            yield return null;
            RemoveAllLayoutGroupsRoutine();
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

        /// <summary>
        /// Removes all layout groups after a frame.
        /// Ensures that the UI elements has been properly positioned
        /// And prevents the canvas runtime layout calculation.
        /// </summary>
        /// <returns></returns>
        private void RemoveAllLayoutGroupsRoutine()
        {
            var layoutGroups = transform.GetComponentsInChildren<LayoutGroup>();
            
            foreach (var layoutGroup in layoutGroups)
            {
                Destroy(layoutGroup);
            }
        }
    }
}
