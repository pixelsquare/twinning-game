using UnityEngine;
using UnityEngine.Events;

namespace PxlSq.Game
{
    public enum GameState
    {
        Menu,
        Game
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;

        public bool IsGameContinued { get; set;}

        public static event UnityAction<GameState> OnGameStateChanged;

        private GameState _gameState;

        public void SetGameState(GameState gameState)
        {
            _gameState = gameState;
            OnGameStateChanged?.Invoke(_gameState);
        }

        public void StartGame()
        {
            IsGameContinued = false;
            SetGameState(GameState.Game);
        }

        public void ContinueGame()
        {
            IsGameContinued = true;
            SetGameState(GameState.Game);
        }

        public void QuitGame()
        {
            SetGameState(GameState.Menu);
            _audioManager.PlaySfx(SfxType.GameOver);
        }
    }
}
