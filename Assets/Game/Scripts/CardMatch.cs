using UnityEngine;

namespace PxlSq.Game
{
    public class CardMatch
    {
        private Card _card1;
        private Card _card2;

        private bool _isMatched;

        public CardMatch(Card card1, Card card2, bool isMatched)
        {
            _card1 = card1;
            _card2 = card2;
            _isMatched = isMatched;

            _card1.OnAnimationFinished += OnAnimationFinished;
            _card2.OnAnimationFinished += OnAnimationFinished;
        }

        ~CardMatch()
        {
            _card1.OnAnimationFinished -= OnAnimationFinished;
            _card2.OnAnimationFinished -= OnAnimationFinished;
        }

        private void OnAnimationFinished(Card card)
        {
            Debug.Log($"{_card1.IsShown} | {_card2.IsShown} | {_isMatched}");

            if (!_card1.IsShown || !_card2.IsShown)
            {
                return;
            }

            _card1.RotateCard();
            _card2.RotateCard();

            _card1.OnAnimationFinished -= OnAnimationFinished;
            _card2.OnAnimationFinished -= OnAnimationFinished;

            if (!_isMatched)
            {
                return;
            }

            _card1.SetActive(false);
            _card2.SetActive(false);
        }
    }
}
