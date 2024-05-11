using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PxlSq.Game
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _turnCountText;
        [SerializeField] private TMP_Text _matchCountText;
        [SerializeField] private TMP_Text _scoreCountText;

        public event UnityAction OnBackButtonClicked;

        public void UpdateTurnCount(uint turnCount)
        {
            _turnCountText.text = $"Turns: {turnCount}";
        }

        public void UpdateMatchCount(uint matchCount)
        {
            _matchCountText.text = $"Matches: {matchCount}";
        }

        public void UpdateScoreCount(uint scoreCount)
        {
            _scoreCountText.text = $"Score: {scoreCount}";
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(HandleBackButtonClicked);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(HandleBackButtonClicked);
        }

        private void HandleBackButtonClicked()
        {
            OnBackButtonClicked?.Invoke();
        }
    }
}
