using UnityEngine;

namespace PxlSq.Game
{
    public class ComboManager : MonoBehaviour
    {
        [SerializeField] private ComboView _comboView;
        [SerializeField] private float _comboDuration = 1f;

        private bool IsComboEnded => _comboCount > 0 && _comboTimer <= 0;

        private uint _comboCount = 0;
        private float _comboTimer = 0f;

        private void OnEnable()
        {
            BoardManager.OnCardMatched += HandleCardMatched;
        }

        private void OnDisable()
        {
            BoardManager.OnCardMatched -= HandleCardMatched;
        }

        private void Start()
        {
            ResetCombo();
        }

        private void Update()
        {
            if (_comboTimer > 0)
            {
                _comboTimer -= Time.deltaTime;
                
                UpdateView(_comboTimer / _comboDuration);

                if (_comboTimer <= 0)
                {
                    ApplyComboPoints();
                    ResetCombo();
                }
            }
        }

        private void ApplyCombo()
        {
            if (IsComboEnded)
            {
                ResetCombo();
                return;
            }

            AddComboCount();
            _comboTimer = _comboDuration;
        }


        private void ResetCombo()
        {
            _comboCount = 0;
        }

        private void ApplyComboPoints()
        {
            var scorePoint = GameConfig.Instance.ScorePoint;
            var comboMultiplier = GameConfig.Instance.ComboMultiplier;
            AddScorePoints((uint)(scorePoint * _comboCount * comboMultiplier));
        }

        private void AddScorePoints(uint scorePoint = 1)
        {
            GameDataManager.Instance.Score += scorePoint;
            SaveGameData();
        }

        private void AddComboCount(uint comboCount = 1)
        {
            _comboCount += comboCount;
        }

        private void UpdateView(float percent)
        {
            _comboView.UpdateComboSlider(percent);
        }

        private void SaveGameData()
        {
            GameDataManager.Instance.SaveGameData();
        }

        private void HandleCardMatched(bool didMatch)
        {
            if (!didMatch)
            {
                return;
            }

            ApplyCombo();
        }
    }
}
