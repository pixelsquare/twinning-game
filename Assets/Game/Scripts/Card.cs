using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PxlSq.Game
{
    /// <summary>
    /// Card view
    /// </summary>
    public class Card : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _logo;
        [SerializeField] private CardRotator _cardRotator;

        public int Index => _index;

        public UnityAction<Card> OnCardClicked;
        public event UnityAction<Card> OnAnimationFinished;

        public bool IsAnimating => _cardRotator.IsRotating;
        public bool IsShown => _isShown;

        private int _index;
        private bool _isShown;

        /// <summary>
        /// Initialies the card view
        /// </summary>
        /// <param name="index"></param>
        /// <param name="logoSprite"></param>
        /// <param name="onCardClicked"></param>
        public void Initialize(int index, Sprite logoSprite, UnityAction<Card> onCardClicked = null)
        {
            OnCardClicked = onCardClicked;
            SetIndex(index);
            SetLogo(logoSprite);
        }

        /// <summary>
        /// Sets the index for the card
        /// </summary>
        /// <param name="index"></param>
        public void SetIndex(int index)
        {
            _index = index;
        }

        /// <summary>
        /// Activates / deactivates the card
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        /// <summary>
        /// Sets the logo for the card
        /// </summary>
        /// <param name="logoSprite"></param>
        public void SetLogo(Sprite logoSprite)
        {
            _logo.sprite = logoSprite;
        }

        /// <summary>
        /// Rotates the card
        /// </summary>
        public void RotateCard()
        {
            _cardRotator?.Rotate();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleCardButtonClicked);
            _cardRotator.OnRotationFinished += HandleCardAnimFinished;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleCardButtonClicked);
            _cardRotator.OnRotationFinished -= HandleCardAnimFinished;
        }

        /// <summary>
        /// Sets the card button interactability.
        /// </summary>
        /// <param name="interactive"></param>
        private void SetButtonInteractable(bool interactive)
        {
            _button.interactable = interactive;
        }

        /// <summary>
        /// Handles the card button clicks
        /// </summary>
        private void HandleCardButtonClicked()
        {
            RotateCard();
            SetButtonInteractable(false);
            OnCardClicked?.Invoke(this);
        }

        /// <summary>
        /// Handles the card animation finish event
        /// </summary>
        private void HandleCardAnimFinished()
        {
            _isShown = !_isShown;
            SetButtonInteractable(true);
            OnAnimationFinished?.Invoke(this);
        }
    }
}
