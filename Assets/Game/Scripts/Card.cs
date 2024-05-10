using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace PxlSq.Game
{
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

        public void Initialize(int index, Sprite logoSprite)
        {
            SetIndex(index);
            SetLogo(logoSprite);
        }

        public void SetIndex(int index)
        {
            _index = index;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetLogo(Sprite logoSprite)
        {
            _logo.sprite = logoSprite;
        }

        public void RotateCard()
        {
            _cardRotator?.Rotate();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnCardButtonClicked);
            _cardRotator.OnRotationFinished += OnCardAnimationFinished;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnCardButtonClicked);
            _cardRotator.OnRotationFinished -= OnCardAnimationFinished;
        }

        private void SetButtonInteractable(bool interactive)
        {
            _button.interactable = interactive;
        }

        private void OnCardButtonClicked()
        {
            RotateCard();
            SetButtonInteractable(false);
            OnCardClicked?.Invoke(this);
        }

        private void OnCardAnimationFinished()
        {
            _isShown = !_isShown;
            SetButtonInteractable(true);
            OnAnimationFinished?.Invoke(this);
        }
    }
}
