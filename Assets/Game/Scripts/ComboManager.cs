using UnityEngine;

namespace PxlSq.Game
{
    /// <summary>
    /// Manages the combo
    /// </summary>
    public class ComboManager : MonoBehaviour
    {
        [SerializeField] private ComboView _comboView;
        [SerializeField] private float _comboDuration = 1f;

        private bool IsComboEnded => _comboCount > 0 && _comboTimer <= 0;

        private uint _comboCount = 0;
        private float _comboTimer = 0f;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += HandleGameStateChanged;
            BoardManager.OnCardMatched += HandleCardMatched;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= HandleGameStateChanged;
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

        /// <summary>
        /// Applies the combo
        /// </summary>
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

        /// <summary>
        /// Resets the combo
        /// </summary>
        private void ResetCombo()
        {
            _comboCount = 0;
            _comboTimer = 0;
        }

        /// <summary>
        /// Apply combo changes to game data
        /// </summary>
        private void ApplyComboPoints()
        {
            var scorePoint = GameConfig.Instance.ScorePoint;
            var comboMultiplier = GameConfig.Instance.ComboMultiplier;
            var baseScorePoints = _comboCount * scorePoint;
            var totalScorePoints = baseScorePoints + (baseScorePoints * (_comboCount - 1) * comboMultiplier);
            AddScorePoints((uint)totalScorePoints);
        }

        /// <summary>
        /// Add score points and save the game data
        /// </summary>
        /// <param name="scorePoint"></param>
        private void AddScorePoints(uint scorePoint = 1)
        {
            GameDataManager.Instance.Score += scorePoint;
            SaveGameData();
        }

        /// <summary>
        /// Add combo count
        /// </summary>
        /// <param name="comboCount"></param>
        private void AddComboCount(uint comboCount = 1)
        {
            _comboCount += comboCount;
        }

        /// <summary>
        /// Updates the combo slider progress
        /// </summary>
        /// <param name="percent"></param>
        private void UpdateView(float percent)
        {
            _comboView.UpdateComboSlider(percent);
        }

        /// <summary>
        /// Saves the game data
        /// </summary>
        private void SaveGameData()
        {
            GameDataManager.Instance.SaveGameData();
        }

        /// <summary>
        /// Checks whether the game is won.
        /// </summary>
        private void CheckGameWon()
        {
            var didWin = GameDataManager.Instance.didWin;

            if (didWin)
            {
                ApplyComboPoints();
                ResetCombo();
            }
        }

        /// <summary>
        /// Handles game state changes
        /// Activates the slider on game phase
        /// </summary>
        /// <param name="gameState"></param>
        private void HandleGameStateChanged(GameState gameState)
        {
            _comboView.SetComboSliderActive(gameState == GameState.Game);
            _comboView.UpdateComboSlider(0f);
        }

        /// <summary>
        /// Handles card match events
        /// </summary>
        /// <param name="didMatch"></param>
        private void HandleCardMatched(bool didMatch)
        {
            if (!didMatch)
            {
                return;
            }

            ApplyCombo();
            CheckGameWon();
        }
    }
}
